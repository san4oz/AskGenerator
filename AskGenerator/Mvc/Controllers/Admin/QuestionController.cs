using AskGenerator.Business.Entities;
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
        public ActionResult Create()
        {            
            return View();
        }

        [HttpGet]
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
            var model = Map<QuestionViewModel, Question>(viewModel);
            Site.QuestionManager.Create(model);
            if(viewModel.IsAboutTeacher)
                return RedirectToAction("TeacherList");
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(QuestionViewModel viewModel)
        {
            var model = Map<QuestionViewModel, Question>(viewModel);
            Site.QuestionManager.Create(model);
            return RedirectToAction("TeacherList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel viewModel)
        {
            var model = Map<QuestionViewModel, Question>(viewModel);
            Site.QuestionManager.Update(model);
            if (viewModel.IsAboutTeacher)
                return RedirectToAction("TeacherList");
            return RedirectToAction("List");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account") }, 403);
            }
            if (!string.IsNullOrEmpty(id))
            {                
                Site.QuestionManager.Delete(id);
            }
#warning ToDO return deleted item.
            return Json(true);
        }
    }
}
