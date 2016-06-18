using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.Controllers;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.ViewModels;
using AutoMapper;
using Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles="admin")]
    public class TeacherController : BaseController
    {
        protected ITeacherManager TeacherManager { get; private set; }

        public TeacherController()
        {
            TeacherManager = Site.TeacherManager;
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var teachers = await TeacherManager.ListAsync();
            var viewModel = Map<IList<Teacher>, IList<TeacherViewModel>>(teachers);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new TeacherComposeViewModel(null);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherComposeViewModel viewModel)
        {
            var teacher = DecomposeStudentViewModel(viewModel.Teacher);
            Site.TeacherManager.Create(teacher, viewModel.Teacher.SelectedGroups);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var teacher = TeacherManager.Get(id);
            if (teacher == null)
                return HttpNotFound("Teacher with specified ID was not found.");
            var viewModel = new TeacherComposeViewModel(Map<Teacher, TeacherViewModel>(teacher));

            IsEditing = true;
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeacherComposeViewModel viewModel)
        {
            var teacher = DecomposeStudentViewModel(viewModel.Teacher);
            Site.TeacherManager.Update(teacher, viewModel.Teacher.SelectedGroups);

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { url = Url.Action("Login", "Account", new { returnUrl = Url.Action("List") }) }, 403);

            if (string.IsNullOrEmpty(id))
                return Json(false);

            return await Task.Factory.StartNew(() =>
            {
                var q = TeacherManager.Extract(id);
                if (q != null)
                {
                    DeleteFile(q.Image);
                    return Json(q);
                }
                return Json(false);
            });
        }

        private Teacher DecomposeStudentViewModel(TeacherViewModel model)
        {
            var teacher = Map<TeacherViewModel, Teacher>(model);
            if(model.ImageFile != null && model.ImageFile.ContentLength > 0)
                teacher.Image = SaveImage(model.ImageFile, model.Id, "teacher");
            return teacher;
        }
    }
}
