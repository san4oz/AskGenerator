using AskGenerator.Business.Entities;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace AskGenerator.Mvc.Controllers
{
    public class DetailsController : BaseController
    {
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public async Task<ActionResult> Team(string id = AskGenerator.Business.Entities.Team.AllTeachersTeamId, int iter = -1)
        {
            var model = new TeamResultsViewModel();
            model.Id = id;

            var manager = Site.TeamManager;
            var teams = await manager.AllAsync();
            if (iter != -1)
                teams.AsParallel().ForAll(t => manager.LoadHistory(t).InitStatistics(iter));
            var team = teams.SingleOrDefault(t => t.Id.Equals(model.Id, StringComparison.InvariantCultureIgnoreCase));
            if (team == null)
            {
                model.Id = AskGenerator.Business.Entities.Team.AllTeachersTeamId;
                team = Site.TeamManager.Get(model.Id);
            }

            var tManager = Site.TeacherManager;
            var teachers = await tManager.AllAsync(false);
            if (model.Id != AskGenerator.Business.Entities.Team.AllTeachersTeamId)
                teachers = teachers.Where(t => t.TeamId.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();
            teachers.AsParallel().ForAll(t => tManager.LoadHistory(t).InitStatistics(iter));

            var questions = InitTeacherListViewModel(teachers, model);

            model.Teams = teams;
            model.Marks = team.Marks;
            model.Rating = team.Rating;

            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);

            return View(model);
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public async Task<ActionResult> Group(string id = "all")
        {
            var model = new GroupStatisticViewModel();
            model.Id = id;
            var groups = await Site.GroupManager.AllAsync();
            var group = groups.SingleOrDefault(g => g.Id == id);
            if (group == null)
            {
                model.Id = "all";
                group = Site.GroupManager.Get(model.Id);
            }

            var questions = await Site.QuestionManager.AllAsync();
            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);

            model.Marks = group.Marks;
            model.Rating = group.Rating;        

            model.Groups = groups;
            return View(model);
        }
    }
}
