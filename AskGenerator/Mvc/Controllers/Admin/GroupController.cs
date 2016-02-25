using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class GroupController : BaseController
    {
        protected IGroupManager GroupManager { get; private set; }

        public GroupController()
        {
            GroupManager = Site.GroupManager;
        }

        [HttpGet]
        public async Task<ActionResult> List( )
        {
            var groups = await Site.GroupManager.AllAsync();
            var viewModel = Map<IList<Group>, IList<GroupViewModel>>(groups);
            return View(viewModel);
        }

        #region Create
        [HttpGet]
        public ActionResult Create( )
        {
            var group = new GroupViewModel();
            return View(group);
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var group = Map<GroupViewModel, Group>(viewModel);
            Site.GroupManager.Create(group);
            return RedirectToAction("List");
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            IsEditing = true;
            var group = Site.GroupManager.Get(id);
            if (group == null)
                return HttpNotFound("Group ({0}) was not found".FormatWith(id));
            return View("Create", Map<Group, GroupViewModel>(group));
        }

        [HttpPost]
        public ActionResult Edit(GroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            var group = Map<GroupViewModel, Group>(viewModel);
            Site.GroupManager.Update(group);
            return RedirectToAction("List");
        }
        #endregion

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { url = Url.Action("Login", "Account", new { returnUrl = Url.Action("List") }) }, 403);

            if (string.IsNullOrEmpty(id))
                return Json(false);

            return await Task.Factory.StartNew(() =>
            {
                var q = GroupManager.Extract(id);
                if (q != null)
                    return Json(q);
                return Json(false);
            });
        }
    }
}
