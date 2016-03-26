using AskGenerator.Helpers;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearCache(string url)
        {
            Response.RemoveOutputCacheItem(url);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SendResult()
        {
            try
            {
                return await Task.Factory.StartNew<ActionResult>((state) =>
                {
                    System.Web.HttpContext.Current = (System.Web.HttpContext)state;
                    var subscribers = Site.Subscribers.All();
                    var emails = subscribers.Where(x => !x.Email.IsEmpty())
                        .Select(x => x.Email)
                        .ToList();

                    var accountEmails = Site.UserManager.Users.Where(u => u.Email != null)
                        .Select(u => u.Email)
                        .ToList();

                    var allEmails = emails.Union(accountEmails);

                    Mailer.Send("ConirmVoite", "semka148@rambler.ru", CreateResultsTags(), allEmails);

                    if (Request.IsAjaxRequest())
                        return Json(true, JsonRequestBehavior.AllowGet);

                    return RedirectToAction("Index");
                }, System.Web.HttpContext.Current);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Recalculate()
        {
            var tqManager = Site.TQManager;
            var voteManager = Site.VoteManager;
            return await Task.Factory.StartNew<ActionResult>(() =>
            {
                try
                {
                    var tqs = tqManager.All();
                    var votes = voteManager.All().ToLookup(v => v.TeacherId);
                    foreach (var tq in tqs)
                    {
                        tq.Answer = tq.Count = 0;
                        foreach (var vote in votes[tq.TeacherId].Where(v => v.QuestionId.Id == tq.QuestionId && v.Answer != 0))
                        {
                            tq.Answer += vote.Answer;
                            tq.Count++;
                        }
                        tq.Answer = tq.Count != 0 ? tq.Answer / tq.Count : 0;
                        tqManager.Update(tq);
                    }
                }
                catch (Exception e)
                {
                    return Json(e, JsonRequestBehavior.AllowGet);
                }
                if (Request.IsAjaxRequest())
                    return Json(true, JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");
            });
        }

        #region protected
        protected Dictionary<string, string> CreateResultsTags()
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://ztu-fikt.azurewebsites.net/");
            result.Add("siteName", "Evaluate");

            return result;
        }
        #endregion
    }
}
