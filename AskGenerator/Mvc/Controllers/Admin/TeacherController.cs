using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.ViewModels;
using AutoMapper;
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
        [HttpGet]
        public ActionResult List()
        {
            var teachers = Site.TeacherManager.All();
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
            var teacher = Site.TeacherManager.Get(id);
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

        private Teacher DecomposeStudentViewModel(TeacherViewModel model)
        {
            var teacher = Map<TeacherViewModel, Teacher>(model);
            if(model.ImageFile != null && model.ImageFile.ContentLength > 0)
                teacher.Image = SaveImage(model.ImageFile, model.Id);
            return teacher;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Students(string teacherId)
        {
            var model = CreateTeacherVotingViewModel(teacherId);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GeneratePDF(string teacherId)
        {
            var teacher = Site.TeacherManager.Get(teacherId);
            if (teacher == null)
                return RedirectToAction("List");

            var parameters = new RouteValueDictionary();

            parameters.Add("teacherId", teacherId);

            var url = this.Url.Action("Students", "Teacher", new { teacherId = teacherId }, this.Request.Url.Scheme);

           
            var pdf = Site.PDFGenerator.Generate(HttpContext.ApplicationInstance.Context,
                string.Format("{0} {1}", teacher.FirstName, teacher.LastName),
                url);

            return pdf;
        }

        public TeacherVotingViewModel CreateTeacherVotingViewModel(string teacherId)
        {
            var model = new TeacherVotingViewModel();

            var teacher = Site.TeacherManager.Get(teacherId);
            model.Teacher = Map<Teacher, TeacherViewModel>(teacher);
     
            var students = Site.TeacherManager.GetRelatedStudents(teacherId);
            model.Students = Map<List<Student>, List<StudentViewModel>>(students);

            model.Vote = new VotingViewModel();

            return model;            
        }
    }
}
