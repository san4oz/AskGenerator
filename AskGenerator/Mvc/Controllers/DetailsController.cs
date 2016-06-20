using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
using AskGenerator.Business.InterfaceDefinitions.Managers;
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
        public async Task<ActionResult> Team(string id = AskGenerator.Business.Entities.Team.AllTeachersTeamId, string i = "")
        {
            var model = new TeamResultsViewModel();
            model.Id = id;

            var iteration = i.IsEmpty() ? null : Site.Settings.General().GetIteration(i);
            if (!i.IsEmpty() && iteration == null)
                return HttpNotFound("Iteration {0} was not found".FormatWith(id));

            var manager = Site.TeamManager;
            var teams = await manager.AllAsync();
            if (iteration != null)
            {
                manager.LoadHistory(teams);
                teams.ForEach(t => t.InitStatistics(iteration.Id));
            }

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

            if (iteration != null)
            {
                tManager.LoadHistory(teachers);
                teachers.AsParallel().ForAll(t => t.InitStatistics(iteration.Id));
            }

            var questions = InitTeacherListViewModel(teachers, model);

            model.Teams = teams;
            model.Marks = team.Marks;
            model.Rating = team.Rating;

            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);

            return View(model);
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public async Task<ActionResult> Group(string id = "all", string i = "")
        {
            var iteration = i.IsEmpty() ? null : Site.Settings.General().GetIteration(i);
            if (!i.IsEmpty() && iteration == null)
                return HttpNotFound("Iteration {0} was not found".FormatWith(id));


            var model = new GroupStatisticViewModel();
            model.Id = id;

            var manager = Site.GroupManager;
            var groups = await manager.AllAsync();
            if (iteration != null)
            {
                manager.LoadHistory(groups);
                groups.AsParallel().ForAll(g => g.InitStatistics(iteration.Id));
            }

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
