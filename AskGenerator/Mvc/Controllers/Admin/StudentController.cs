using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    public class StudentController : BaseController
    {
        [HttpGet]
        public ActionResult List()
        {
            var students = Site.StudentManager.All();
            var viewModel = Map<IList<Student>, IList<StudentViewModel>>(students);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = CreateCompositeModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateStudentViewModel model)
        {
            var student = DecomposeStudentViewModel(model);            
            Site.StudentManager.Create(student);
            return RedirectToAction("List");
        }

        private CreateStudentViewModel CreateCompositeModel()
        {
            var student = new StudentViewModel();
            var groups = Site.GroupManager.All();
            var groupViewModels = Map<IList<Group>, IList<GroupViewModel>>(groups);
            var viewModel = new CreateStudentViewModel();
            viewModel.Student = student;
            viewModel.Groups = groupViewModels;
            return viewModel;
        }

        private Student DecomposeStudentViewModel(CreateStudentViewModel model)
        {
            var student = Map<StudentViewModel, Student>(model.Student);
            var group = Site.GroupManager.Get(model.Student.Group.Id);
            student.Group = group;
            student.Image = SavePhoto(model.Student.ImageFile, model.Student.Id);
            return student;
        }

        private string SavePhoto(HttpPostedFileBase photo, string studentId)
        {
            if (photo == null || photo.ContentLength <= 0)
                return null;

            var path = string.Format("/Content/Images/{0}{1}", studentId, Path.GetExtension(photo.FileName));

            var serverPath = Server.MapPath(path);

            photo.SaveAs(serverPath);

            return path;

        }
    }
}
