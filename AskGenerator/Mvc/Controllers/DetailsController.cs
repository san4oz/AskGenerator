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
        [OutputCache(CacheProfile = "Cache1Hour")]
        public async Task<ActionResult> Team(string id)
        {
            var model = new TeamResultsViewModel();
            model.Id = id;
            var teachers = await Site.TeacherManager.AllAsync(true);

            if (model.Id.IsEmpty() || Site.TeamManager.Get(model.Id) == null)
            {
                model.Id = "all";
            }
            else
            {
                teachers = teachers.Where(t => t.TeamId.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            var teachersIds = teachers.ToDictionary(t => t.Id);
            var questions = InitTeacherListViewModel(teachers, model);

            var allVotes = await Site.VoteManager.AllAsync();

            int maxCount = int.MinValue;
            float avgSum = 0;
            foreach(var group in allVotes.Where(m => teachersIds.ContainsKey(m.TeacherId)).GroupBy(m => m.QuestionId))
            {
                int count = 0;
                var qDictionary = model.Marks.GetOrCreate(group.Key.Id);

                foreach(var vote in group){
                    qDictionary[vote.Answer] = qDictionary.GetOrDefault(vote.Answer) + 1;
                    count++;
                }

                float avg = qDictionary.Aggregate(0f, (a, p) => a = a + p.Key * p.Value)/count;
                qDictionary.Avg.Answer = avg;
                qDictionary.Avg.Count = count;

                avgSum += avg;
                if (count > maxCount)
                    maxCount = count;
            }

            model.Teams = await Site.TeamManager.AllAsync();
            model.Questions = questions.ToDictionary(q => q.Id, q => q.QuestionBody);
            var difficult = model.Marks[questions.First().Id].Avg;
            model.Rate = new Mark()
            {
                Answer = CalculateRate(difficult.Answer, (avgSum - difficult.Answer) / (model.Marks.Count - 1), maxCount),
                Count = maxCount
            };

            return View(model);
        }
    }
}
