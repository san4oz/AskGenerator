using AskGenerator.Business.Entities;
using AskGenerator.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Controllers
{
    public class DetailsController : BaseController
    {
        public async Task<ActionResult> Team(string id)
        {
            var model = new TeamResultsViewModel();
            model.Id = id;

            var teachers = await Site.TeacherManager.AllAsync(true);

            teachers = teachers.Where(t => t.TeamId.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();
            var teachersIds = teachers.ToDictionary(t => t.Id);
            var questions = InitTeacherListViewModel(teachers, model);

            var allVotes = await Site.VoteManager.AllAsync();
            var marks = new Dictionary<string, IDictionary<short, int>>(questions.Count);

            int maxCount = int.MinValue;
            float avgSum = 0;
            foreach(var group in allVotes.Where(m => teachersIds.ContainsKey(m.TeacherId)).GroupBy(m => m.QuestionId))
            {
                int count = 0;
                var qDictionary = marks.GetOrDefault(group.Key.Id);
                if (qDictionary == null)
                    marks[group.Key.Id] = qDictionary = new Dictionary<short, int>();

                foreach(var vote in group){
                    qDictionary[vote.Answer] = qDictionary.GetOrDefault(vote.Answer) + 1;
                    count++;
                }

                float avg = qDictionary.Aggregate(0f, (a, p) => a = a + p.Key * p.Value)/count;
                model.Avgs[group.Key.Id] = new Mark() { Answer = avg, Count = count };

                avgSum += avg;
                if (count > maxCount)
                    maxCount = count;
            }

            model.Teams = await Site.TeamManager.AllAsync();
            model.Marks = marks;
            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);
            var difficult = model.Avgs[questions.First().Id];
            model.Avgs["rate"] = new Mark()
            {
                Answer = CalculateRate(difficult.Answer, (avgSum - difficult.Answer) / (model.Avgs.Count - 1), maxCount),
                Count = maxCount
            };

            return View(model);
        }
    }
}
