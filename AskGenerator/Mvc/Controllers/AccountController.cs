﻿using System;
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
using Resources;

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
                return Site.UserManager;
            }
        }

        protected RoleManager RoleManager
        {
            get
            {
                return Site.RoleManager;
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
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            User user;
            
            if (!model.Key.IsEmpty())
            {
                var keyError = ModelState.GetOrDefault("Key");
                if (keyError != null && keyError.Errors.Count == 0)
                {
                    ModelState.Clear();
                    user = await Manager.FindByLoginKeyAsync(model.Key);
                    if (user == null)
                        ModelState.AddModelError("Key", Resource.WrongLoginKey);
                    else
                    {
                        if (!await CheckCaptcha())
                        {
                            ModelState.AddModelError("Key", Resource.ConfirmNoRobot);
                            return View(model);
                        }
                        return await Login(user, returnUrl, model.IsPersistent);
                    }
                }
            }
            else if (ModelState.IsValid)
            {
                model.Email = TransformEmail(model.Email);
                user = await Manager.FindByEmailAsync(model.Email);
                if (user == null || !Manager.CheckPassword(user, model.Password))
                    ModelState.AddModelError("Password", Resource.WrongEmailOrPassword);
                else
                    return await Login(user, returnUrl, model.IsPersistent);
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        protected async Task<ActionResult> Login(User user, string returnUrl = null, bool isPersistent = false)
        {
            var identity = await Manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = isPersistent
            }, identity);

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Register
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var task = checkLastNameAsync(model.LastName, model.GroupId);
                var student = task != null ? await task : null;
                if (student == null || student.HasUserAccount)
                {
                    ModelState.AddModelError("LastName", Resource.NoLastNameFound);
                    return View(model);
                }
                if (!await CheckCaptcha())
                {
                    ModelState.AddModelError("", Resource.ConfirmNoRobot);
                    return View(model);
                }

                User user = null;
                if(!student.AccountId.IsEmpty())
                    user = await Manager.FindByIdAsync(student.AccountId);

                if (user != null)
                {
                    user.GroupId = model.GroupId;
                    user.Email = model.Email;
                }
                else
                {
                    model.Email = TransformEmail(model.Email);
                    user = Map<RegistrationModel, User>(model);
                }
                user.StudentId = student.Id;

                IdentityResult result = await Manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    student.HasUserAccount = true;
                    Site.StudentManager.Update(student);
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

        public JsonResult CheckLastName(string lastName, string groupId)
        {
            var student = checkLastName(lastName, groupId);
            bool result = student == null ? false : !student.HasUserAccount;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Confirm(string id, string param = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                return HttpNotFound();

            var user = await Manager.FindByIdAsync(id);
            if (user == null || user.EmailConfirmed)
                return HttpNotFound();

            user.EmailConfirmed = true;
            var task = Manager.AddToRoleAsync(user.Id, Role.Admin);
            task = Manager.UpdateAsync(user);

            return View();
        }
        #endregion

        protected Task<Student> checkLastNameAsync(string lastName, string groupId)
        {
            var manager = Site.StudentManager;
            if (!lastName.IsEmpty())
            {
                return Task.Factory.StartNew(() =>
                {
                    lastName = lastName.ToUpperInvariant();
                    var students = manager.GroupList(groupId);
                    return students.FirstOrDefault(s => s.LastName.ToUpperInvariant() == lastName);
                });
            }

            return null;
        }

        protected Student checkLastName(string lastName, string groupId)
        {
            if (!lastName.IsEmpty())
            {
                lastName = lastName.ToUpperInvariant();
                var students = Site.StudentManager.GroupList(groupId);
                return students.FirstOrDefault(s => s.LastName.ToUpperInvariant() == lastName);
            }

            return null;
        }

        protected string TransformEmail(string email)
        {
            var t = email.ToLower().Split('@');
            return t[0].Replace(".", string.Empty) + '@' + t[1];
        }

        protected Dictionary<string, string> CreateConfirmTags(string id)
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://ztu-fikt.azurewebsites.net/");
            result.Add("siteName", "Evaluate");
            result.Add("confirmURL", HttpContext.Request.Url.GetLeftPart(UriPartial.Authority).TrimEnd('/') + Url.Action("Confirm", new { id = id }));
            result.Add("vkURL", @Resource.vkURL);
            result.Add("fbURL", @Resource.fbURL);
            result.Add("vkdekURL", @Resource.vkdekURL);
            result.Add("nomURL", @Resource.nomURL);
            result.Add("bestURL", @Resource.bestURL);

            return result;
        }
    }
}
