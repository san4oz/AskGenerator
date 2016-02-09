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
using Microsoft.Owin.Security;

namespace AskGenerator.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        const string ConirmRegistrationMail = "ConirmRegistration";

        #region Managers
        protected UserManager Manager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
        }

        protected RoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<RoleManager>();
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Login
        public ActionResult Login(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Email = TransformEmail(model.Email);
                var user = await Manager.FindByEmailAsync(model.Email);
                if (user == null || !Manager.CheckPassword(user, model.Password))
                {
                    ModelState.AddModelError("Password", "Невірна електронна адреса чи пароль.");
                }
                else
                {
                    var identity = await Manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.IsPersistent
                    }, identity);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Register
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
                if (!await CheckCaptcha())
                {
                    ModelState.AddModelError("", "Підтвердіть, що ви не робот.");
                    return View(model);
                }

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

        public async Task<ActionResult> Confirm(string id, string param = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                return HttpNotFound();

            var user = await Manager.FindByIdAsync(id);
            if (user == null ||  user.EmailConfirmed)
                return HttpNotFound();

            user.EmailConfirmed = true;
            var task = Manager.AddToRoleAsync(user.Id, Role.Admin);
            task = Manager.UpdateAsync(user);

            return View();
        }
        #endregion

        protected string TransformEmail(string email)
        {
            var t = email.ToLower().Split('@');
            return t[0].Replace(".", string.Empty) + '@' + t[1];
        }

        protected Dictionary<string, string> CreateConfirmTags(string id)
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://google.com.ua");
            result.Add("siteName", "Evaluate google");
            result.Add("confirmURL", HttpContext.Request.Url.GetLeftPart(UriPartial.Authority).TrimEnd('/') + Url.Action("Confirm", new { id = id }));

            return result;
        }
    }
}
