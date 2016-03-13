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
using Resources;
using Microsoft.AspNet.Identity.EntityFramework;
using AskGenerator.Mvc.Controllers;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles="admin")]
    public class AccountController : BaseController
    {
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

        [HttpGet]
        public async Task<ActionResult> Edit(string id, string returnUrl = null)
        {
            if (id.IsEmpty())
                throw new ArgumentNullException("Account ID should be specified.", "id");
            var user = await Manager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound("Account '{0}' was not found".FormatWith(id));

            ViewBag.ReturnUrl = returnUrl;
            return View(Map<User, UserViewModel>(user));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string returnUrl, UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            id = id.Or(model.Id);
            if (id.IsEmpty())
                throw new ArgumentNullException("Account ID should be specified.", "id");
            var user = await Manager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound("Account '{0}' was not found".FormatWith(id));

            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            user.GroupId = model.GroupId;
            user.LoginKey = model.LoginKey;
            user.StudentId = model.StudentId;
            if(!model.Password.IsEmpty())
                user.PasswordHash = Manager.PasswordHasher.HashPassword(model.Password);
            await Manager.UpdateAsync(user);

            if (returnUrl.IsEmpty())
                return RedirectToAction("List", "Student", new { area = "Admin" });

            return Redirect(returnUrl);
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
