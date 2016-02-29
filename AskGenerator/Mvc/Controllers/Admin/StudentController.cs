using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.Parsers;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class StudentController : BaseController
    {
        protected IStudentManager StudentManager { get; private set; }

        public StudentController()
        {
            StudentManager = Site.StudentManager;
        }
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var students = await StudentManager.AllAsync();
            var viewModel = Map<IList<Student>, IList<StudentViewModel>>(students);
            return View(viewModel);
        }

        #region Create
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
            StudentManager.Create(student);
            return RedirectToAction("List");
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var student = Site.StudentManager.Get(id);
            var model = CreateCompositeModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var edited = DecomposeStudentViewModel(model);
            StudentManager.Update(edited);
            return RedirectToAction("List");
        }
        #endregion

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
                var q = StudentManager.Extract(id);
                if (q != null)
                {
                    DeleteImage(q.Image);
                    return Json(q);
                }
                return Json(false);
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Import(HttpPostedFileBase file)
        {
            if (file == null || !file.FileName.EndsWith(".txt"))
                return Json("Use .txt", 500);
            var parser = new StudentTextParser(Site.GroupManager, Site.StudentManager);

            return await Task.Factory.StartNew(() =>
            {
                parser.ParseStream(file.InputStream);
                return Json(parser.Info);
            });

        }

        private CreateStudentViewModel CreateCompositeModel(string studentId = null)
        {
            StudentViewModel student;

            if (string.IsNullOrEmpty(studentId))
                student = new StudentViewModel();
            else
                student = Map<Student, StudentViewModel>(Site.StudentManager.Get(studentId));

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
            student.Image = SaveImage(model.Student.ImageFile, model.Student.Id);
            return student;
        }
    }
}
