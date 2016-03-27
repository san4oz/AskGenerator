using AskGenerator.Business.Entities;
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

            return await Task.Factory.StartNew<ActionResult>((context) =>
            {
                System.Web.HttpContext.Current = (System.Web.HttpContext)context;
                try
                {
                    var tqs = tqManager.All();
                    var voteList = voteManager.All();
                    var votes = voteList.ToLookup(v => v.TeacherId);
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
                    UpdateGroupStaytistic(voteList);
                }
                catch (Exception e)
                {
                    return Json(e, JsonRequestBehavior.AllowGet);
                }
                if (Request.IsAjaxRequest())
                    return Json(true, JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");
            }, System.Web.HttpContext.Current);
        }



        #region protected
        protected Dictionary<string, string> CreateResultsTags()
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://ztu-fikt.azurewebsites.net/");
            result.Add("siteName", "Evaluate");

            return result;
        }

        protected void UpdateGroupStaytistic(IList<Vote> votes)
        {
            var studentVotes = votes.ToLookup(x => x.AccountId);
            var groups = Site.GroupManager.All();
            foreach (var group in groups)
            {
                var avgAnswers = new Dictionary<string, Mark>(7);
                var students = Site.StudentManager.GroupList(group.Id);
                foreach (var student in students)
                {
                    if (student.AccountId.IsEmpty())
                        continue;

                    foreach(var vote in studentVotes[student.AccountId])
                    {
                        var mark = avgAnswers.GetOrCreate(vote.QuestionId.Id);
                        mark.Count++;
                        mark.Answer += vote.Answer;
                        avgAnswers[vote.QuestionId.Id] = mark;
                    }
                }
                float avg = 0;
                int maxCount = int.MinValue;
                foreach (var vote in avgAnswers.Values)
                {
                    if (vote.Count > maxCount) maxCount = vote.Count;
                    avg += (vote.Answer / (float)vote.Count);
                }
                if (avgAnswers.Count == 0)
                {
                    group.VotesCount = 0;
                    group.Avg = 0;
                }
                else
                {
                    group.VotesCount = maxCount;
                    group.Avg = avg / avgAnswers.Count;
                }
                Site.GroupManager.Update(group);
            }
        }
        #endregion
    }
}
