using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
using AskGenerator.Mvc.ViewModels;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = Role.Admin)]
    public class FacultyController : BaseController
    {
        [HttpGet]
        public ActionResult List()
        {
            var models = Site.FacultyManager.All();
            var viewModels = MapList<Faculty, FacultyViewModel>(models);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var faculty = Site.FacultyManager.Get(id);
            if (faculty == null)
                return HttpNotFound("Faculty ({0}) was not found.".FormatWith(id));
            var model = Map<Faculty, FacultyViewModel>(faculty);
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacultyViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var model = Map<FacultyViewModel, Faculty>(viewModel);
            Site.FacultyManager.Create(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacultyViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            var model = Map<FacultyViewModel, Faculty>(viewModel);
            Site.FacultyManager.Update(model);
            return RedirectToAction("List");
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json404("ID was not specified.");

            var model = await Site.FacultyManager.ExtractAsync(id);
            if (model == null)
                return Json404("Faculty was not found.");

            return Json(Map<Faculty, FacultyViewModel>(model));
        }
    }
}
