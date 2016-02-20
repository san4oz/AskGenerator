using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
using AskGenerator.Mvc.ViewModels;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class TeamController : BaseController
    {
        [HttpGet]
        public ActionResult List()
        {
            var models = Site.TeamManager.All();
            var viewModels = MapList<Team, TeamViewModel>(models);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = Map<Team, TeamViewModel>(Site.TeamManager.Get(id));
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel viewModel)
        {
            var model = Map<TeamViewModel, Team>(viewModel);
            Site.TeamManager.Create(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamViewModel viewModel)
        {
            var model = Map<TeamViewModel, Team>(viewModel);
            Site.TeamManager.Update(model);
            return RedirectToAction("List");
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json404("ID was not specified.");

            var team = await Site.TeamManager.Extract(id);
            if (team == null)
                return Json404("Team was not found.");

            return Json(Map<Team, TeamViewModel>(team));
        }
    }
}
