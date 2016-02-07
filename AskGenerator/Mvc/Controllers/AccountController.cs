using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AskGenerator.DataProvider;
using AskGenerator.ViewModels;
using AskGenerator.Business.Entities;
using AskGenerator.Helpers;

namespace AskGenerator.Controllers
{
    public class AccountController : BaseController
    {
        const string ConirmRegistrationMail = "ConirmRegistration";
        protected UserManager Manager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
        }

        public ActionResult Login( )
        {
            return View();
        }

        public ActionResult Register( )
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                model.Email = TransformEmail(model.Email);
                var user = Map<RegistrationModel, User>(model);
                IdentityResult result = await Manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Mailer.Send(ConirmRegistrationMail, model.Email, CreateConfirmTags(model.Id));
                    return View("_Success");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        protected string TransformEmail(string email)
        {
            var t = email.ToLower().Split('@');
            return t[0].Replace(".", string.Empty) + '@' + t[1];
        }

        public async Task<ActionResult> Confirm(string id, string param = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                return HttpNotFound();

            var user = await Manager.FindByIdAsync(id);
            if (user == null ||  user.EmailConfirmed)
                return HttpNotFound();

            user.EmailConfirmed = true;
            await Manager.UpdateAsync(user);

            return View();
        }

        Dictionary<string, string> CreateConfirmTags(string id)
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://google.com.ua");
            result.Add("siteName", "Evaluate google");
            result.Add("confirmURL", HttpContext.Request.Url.GetLeftPart(UriPartial.Authority).TrimEnd('/') + Url.Action("Confirm", new { id = id }));

            return result;
        }
    }
}
