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
    public class QuestionController : BaseController
    {
        protected IQuestionManager QuestionManager { get; private set; }

        public QuestionController()
        {
            QuestionManager = Site.QuestionManager;
        }

        [HttpGet]
        public ActionResult List(bool isAboutTeacher = false)
        {
            var models = Site.QuestionManager.List(isAboutTeacher);
            var viewModels = MapList<Question, QuestionViewModel>(models);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult TeacherList()
        {
            return List(true);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Create()
        {
            var model = new QuestionViewModel();
            return View(model);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult CreateTeacher()
        {
            var model = new QuestionViewModel()
            {
                IsAboutTeacher = true
            };
            return View("Create", model);
        }


        public ActionResult Edit(string id)
        {
            var model = Map<Question, QuestionViewModel>(Site.QuestionManager.Get(id));
            IsEditing = true;
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var model = DecomposeQuestionViewModel(viewModel);
            Site.QuestionManager.Create(model);
            if (viewModel.IsAboutTeacher)
                return RedirectToAction("TeacherList");
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(QuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var model = DecomposeQuestionViewModel(viewModel);
            Site.QuestionManager.Create(model);
            return RedirectToAction("TeacherList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            var model = DecomposeQuestionViewModel(viewModel);
            Site.QuestionManager.Update(model);
            if (viewModel.IsAboutTeacher)
            {
                Response.RemoveOutputCacheItem("/");
                return RedirectToAction("TeacherList");
            }
            return RedirectToAction("List");
        }

        private Question DecomposeQuestionViewModel(QuestionViewModel model)
        {
            var question = Map<QuestionViewModel, Question>(model);
            if (model.LeftLimit != null && model.LeftLimit.ImageFile != null && model.LeftLimit.ImageFile.ContentLength > 0)
                question.LeftLimit.Image = SaveImage(model.LeftLimit.ImageFile, model.Id + 'l', "question");

            if (model.RightLimit != null && model.RightLimit.ImageFile != null && model.RightLimit.ImageFile.ContentLength > 0)
                question.RightLimit.Image = SaveImage(model.RightLimit.ImageFile, model.Id + 'r', "question");
            return question;
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { url = Url.Action("Login", "Account", new { returnUrl = Url.Action("TeacherList") }) }, 403);
            if (string.IsNullOrEmpty(id))
                return Json(false);

            return await Task.Factory.StartNew(() =>
            {
                var q = QuestionManager.Extract(id);
                if (q != null)
                {
                    base.DeleteImage(q.LeftLimit.Image);
                    base.DeleteImage(q.RightLimit.Image);
                    return Json(q);
                }
                return Json(false);
            });
        }
    }
}
