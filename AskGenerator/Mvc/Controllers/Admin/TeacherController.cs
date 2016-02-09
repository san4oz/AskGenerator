using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
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
            var viewModel = new TeacherComposeViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TeacherComposeViewModel viewModel)
        {           
            var teacher = Map<TeacherViewModel, Teacher>(viewModel.Teacher);

            Site.TeacherManager.Create(teacher, viewModel.Teacher.SelectedGroups);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Students(string teacherId)
        {
            var students = Site.TeacherManager.GetRelatedStudents(teacherId);
            var viewModel = Map<List<Student>, List<StudentViewModel>>(students);
            return View(viewModel);
        }

        [HttpPost]
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
    }
}
