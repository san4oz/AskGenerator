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
        public async Task<ActionResult> Team(string id)
        {
            var model = new TeamResultsViewModel();
            model.Id = id;
            var teachers = await Site.TeacherManager.AllAsync(false);

            if (model.Id.IsEmpty() || Site.TeamManager.Get(model.Id) == null)
                model.Id = "all";
            else
                teachers = teachers.Where(t => t.TeamId.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();

            var teachersIds = teachers.ToDictionary(t => t.Id);
            var questions = InitTeacherListViewModel(teachers, model);

            var allVotes = await Site.VoteManager.AllAsync();

            InitModel(allVotes.Where(m => teachersIds.ContainsKey(m.TeacherId)).GroupBy(m => m.QuestionId.Id),
                model,
                questions.First().Id);
            model.Teams = await Site.TeamManager.AllAsync();
            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);

            return View(model);
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public async Task<ActionResult> Group(string id)
        {
            var model = new GroupStatisticViewModel();
            model.Id = id;
            IList<Student> students;
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

        protected void InitModel(IEnumerable<IGrouping<string, Vote>> votes, IRateble model, string difficultId)
        {
            int maxCount = int.MinValue;
            float avgSum = 0;
            foreach (var group in votes)
            {
                int count = 0;
                var qDictionary = model.Marks.GetOrCreate(group.Key);

                foreach (var vote in group)
                {
                    qDictionary[vote.Answer] = qDictionary.GetOrDefault(vote.Answer) + 1;
                    count++;
                }

                float avg = qDictionary.Aggregate(0f, (a, p) => a = a + p.Key * p.Value) / count;
                qDictionary.Avg.Answer = avg;
                qDictionary.Avg.Count = count;

                avgSum += avg;
                if (count > maxCount)
                    maxCount = count;
            }
            var difficult = model.Marks[difficultId].Avg;
            model.Rating = new Mark()
            {
                Answer = CalculateRate(difficult.Answer, (avgSum - difficult.Answer) / (model.Marks.Count - 1), maxCount),
                Count = maxCount
            };
        }
    }
}
