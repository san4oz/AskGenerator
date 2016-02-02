using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers
{
    public class TeacherController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            var teachers = Site.TeacherManager.All();
            var viewModel = Mapper.Map<IList<Teacher>, IList<TeacherViewModel>>(teachers);

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
            var teacher = Mapper.Map<TeacherViewModel, Teacher>(viewModel.Teacher);

            Site.TeacherManager.Create(teacher, viewModel.Teacher.SelectedGroups);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Students(string teacherId)
        {
            var students = Site.TeacherManager.GetRelatedStudents(teacherId);
            var viewModel = Mapper.Map<List<Student>, List<StudentViewModel>>(students);
            return View(viewModel);
        }
    }
}
