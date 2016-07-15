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
    [Authorize(Roles=Role.Admin)]
    public class AccountController : BaseController
    {
        #region Managers
        protected UserManager Manager
        {
            get
            {
                return (UserManager)Site.UserManager;
            }
        }

        protected RoleManager RoleManager
        {
            get
            {
                return (RoleManager)Site.RoleManager;
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

        /*
        /// <summary>
        /// Renders a view containing the list of 
        /// </summary>
        /// <returns></returns>
        [ActionName("Index")]
        public async Task<ActionResult> List()
        {

        }
        */

        [HttpGet]
        public async Task<ActionResult> Edit(string id, string returnUrl = null)
        {
            if (id.IsEmpty())
                throw new ArgumentNullException("Account ID should be specified.", "id");
            var user = await Manager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound("Account '{0}' was not found".FormatWith(id));

            ViewBag.ReturnUrl = returnUrl;
            var model = Map<User, UserViewModel>(user);

            if(Manager.IsInRole(user.Id, Role.FacultyAdmin))
            {
                model.FacultyId = model.GroupId;
                model.GroupId = string.Empty;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string returnUrl, UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.FacultyId.IsEmpty() && model.GroupId.IsEmpty())
            {
                ModelState.AddModelError("GroupId", Resource.FirstOrSecondRequired.FormatWith(Resource.Group, Resource.Faculty));
                return View(model);
            }

            id = id.Or(model.Id);
            if (id.IsEmpty())
                throw new ArgumentNullException("id", "Account ID should be specified.");
            var user = await Manager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound("Account '{0}' was not found".FormatWith(id));

            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;

            await ProcessFacultyAndGroupIds(model, user);

            user.LoginKey = model.LoginKey;
            user.StudentId = model.StudentId;
            if(!model.Password.IsEmpty())
                user.PasswordHash = Manager.PasswordHasher.HashPassword(model.Password);
            await Manager.UpdateAsync(user);

            if (returnUrl.IsEmpty())
                return RedirectToAction("List", "Student", new { area = "Admin" });

            return Redirect(returnUrl);
        }

        /// <summary>
        /// Choose which ID (Group or Faculty) to save and resolves faculty admins role.
        /// </summary>
        /// <param name="model">The user model to get IDs from.</param>
        /// <param name="user">User entity to save ID to.</param>
        /// <returns><see cref="T:Task"/>.</returns>
        protected virtual async Task ProcessFacultyAndGroupIds(UserViewModel model, Business.Entities.User user)
        {
            var isFacultyAdmin = await Manager.IsInRoleAsync(user.Id, Role.FacultyAdmin);
            if (model.FacultyId.IsEmpty())
            {
                user.GroupId = model.GroupId;
                if (isFacultyAdmin)
                    await Manager.RemoveFromRoleAsync(user.Id, Role.FacultyAdmin);
            }
            else
            {
                user.GroupId = model.FacultyId;
                if (!isFacultyAdmin)
                    await Manager.AddToRoleAsync(user.Id, Role.FacultyAdmin);
            }
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
