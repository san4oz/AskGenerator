using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.Parsers;
using AskGenerator.DataProvider;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.Mvc.Components.Attributes;

namespace AskGenerator.Controllers.Admin
{
    [RolesAuthorize(Role.Admin, Role.FacultyAdmin)]
    public class StudentController : BaseController
    {
        protected IStudentManager StudentManager { get; private set; }
        protected UserManager UserManager { get; private set; }

        public StudentController()
        {
            StudentManager = Site.StudentManager;
            UserManager = (UserManager)Site.UserManager;
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
            var viewModel = new StudentViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel model)
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
            if (id.IsEmpty())
                return HttpNotFound("Student ID was not specified.");
            var student = Site.StudentManager.Get(id);
            if(student == null)
                return HttpNotFound("Student ('{0}') was not specified.".FormatWith(id));

            var model = Map<Student, StudentViewModel>(student);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var edited = DecomposeStudentViewModel(model);
            if (!User.IsAdmin() && !edited.Group.FacultyId.iEquals(User.Identity.GetGroupId()))
            {
                ModelState.AddModelError("GroupId", "Forbidden group.");
                return View(model);
            }
            StudentManager.Update(edited);
            return RedirectToAction("List");
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { url = Url.Action("Login", "Account", new { returnUrl = Url.Action("List") }) }, 403);

            if (string.IsNullOrEmpty(id))
                return Json(false);

            var s = await StudentManager.GetAsync(id);
            if (!s.AccountId.IsEmpty())
            {
                var u = await UserManager.FindByIdAsync(s.AccountId);
                if (u != null)
                    await UserManager.DeleteAsync(u);

            }
            var q = await StudentManager.ExtractAsync(id);
            if (q != null)
            {
                DeleteFile(q.Image);
                return Json(q);
            }
            return Json(false);

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

        public async Task<ActionResult> AccountKeys()
        {
            var groups = await Site.GroupManager.AllAsync();
            var users = UserManager.Users.ToDictionary(u => u.Id);
            var model = new List<GroupLoginKeys>(groups.Count);
            foreach (var g in groups)
            {
                var list = new GroupLoginKeys(g.Students.Count) { GroupName = g.Name };
                foreach (var s in g.Students)
                {
                    if (s.AccountId.IsEmpty())
                        continue;

                    var user = users.GetOrDefault(s.AccountId);
                    if (user == null || user.LoginKey.IsEmpty())
                        continue;
                    list.Add(new StudentKeyPair() { Name = s.GetShortName(), Key = user.LoginKey });
                }
                model.Add(list);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RolesAuthorize(Role.Admin)]
        public async Task<ActionResult> ResetAccountKeys()
        {
            var list = await StudentManager.AllAsync();
            var users = UserManager.Users.ToDictionary(u => u.Id);
            var i = 0;
            foreach (var s in list.Shuffle())
            {
                var user = users.GetOrDefault(s.AccountId);
                if (user == null)
                {
                    user = new User(s.Group.Id, s.Id);
                    user.GenerateLoginKey(i);
                    var result = await UserManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        s.AccountId = user.Id;
                        StudentManager.Update(s);
                    }
                    else
                    {
                        return CreateErrorResponse(result.Errors, s);
                    }
                }
                else
                {
                    user.GenerateLoginKey(i);
                    var result = await UserManager.UpdateAsync(user);
                    if (!result.Succeeded)
                        return CreateErrorResponse(result.Errors, s);
                }
                i++;
            }
            return RedirectToAction("AccountKeys");
        }

        #region private
        
        private Student DecomposeStudentViewModel(StudentViewModel model)
        {
            var existing = Site.StudentManager.Get(model.Id);
            var student = Map<StudentViewModel, Student>(model);
            var group = Site.GroupManager.Get(model.GroupId);
            student.Group = group;
            student.Image = SaveImage(model.ImageFile, model.Id).Or(model.Image);
            if (!User.IsAdmin())
            {
                student.AccountId = existing.AccountId;
                student.HasUserAccount = existing.HasUserAccount;
            }
            return student;
        }

        private JsonResult CreateErrorResponse(IEnumerable<string> errors, Student student)
        {
            var sb = new StringBuilder(("An error occured while reseting key for user {0}" + Environment.NewLine + "Errors: ").FormatWith(student.Id));
            foreach (var l in errors)
                sb.AppendLine(l);
            return Json(sb.ToString(), 505);
        }
        #endregion
    }
}
