using AskGenerator.Business.Entities;
using AskGenerator.Helpers;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class HomeController : AskGenerator.Mvc.Controllers.BaseController
    {
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Index()
        {
          return View();
        }

        public ActionResult ClearCache(string url)
        {
            Response.RemoveOutputCacheItem(url);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SendResult()
        {
            try
            {
                return await Task.Factory.StartNew<ActionResult>((state) =>
                {
                    System.Web.HttpContext.Current = (System.Web.HttpContext)state;
                    var subscribers = Site.Subscribers.All();
                    var emails = subscribers.Where(x => !x.Email.IsEmpty())
                        .Select(x => x.Email)
                        .ToList();

                    var accountEmails = Site.UserManager.Users.Where(u => u.Email != null)
                        .Select(u => u.Email)
                        .ToList();

                    var allEmails = emails.Union(accountEmails);

                    Mailer.Send("ConirmVoite", "semka148@rambler.ru", CreateResultsTags(), allEmails);

                    if (Request.IsAjaxRequest())
                        return Json(true, JsonRequestBehavior.AllowGet);

                    return RedirectToAction("Index");
                }, System.Web.HttpContext.Current);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #region Recalculate
        public async Task<ActionResult> Recalculate()
        {
            var tqManager = Site.TQManager;
            var voteManager = Site.VoteManager;

            return await Task.Factory.StartNew<ActionResult>((context) =>
            {
                System.Web.HttpContext.Current = (System.Web.HttpContext)context;
                try
                {
                    var tqs = tqManager.All();
                    var voteList = voteManager.All();
                    var votes = voteList.ToLookup(v => v.TeacherId);
                    foreach (var tq in tqs)
                    {
                        tq.Answer = tq.Count = 0;
                        foreach (var vote in votes[tq.TeacherId].Where(v => v.QuestionId.Id == tq.QuestionId && v.Answer != 0))
                        {
                            tq.Answer += vote.Answer;
                            tq.Count++;
                        }
                        tq.Answer = tq.Count != 0 ? tq.Answer / tq.Count : 0;
                    }
                    tqManager.Update(tqs);
                    UpdateGroupStaytistic(voteList);
                    var teachers = UpdateBadges();
                    UpdateTeams(teachers);
                }
                catch (Exception e)
                {
                    return Json(e, JsonRequestBehavior.AllowGet);
                }
                if (Request.IsAjaxRequest())
                    return Json(true, JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");
            }, System.Web.HttpContext.Current);
        }

        protected List<Teacher> UpdateBadges()
        {
            var teachers = Site.TeacherManager.All(true);
            var questions = Site.QuestionManager.All();
            var badges = CreateBadges(questions);
            var diffId = questions.First().Id;

            teachers.ForEach(t => t.Badges.Clear());

            foreach (var question in questions)
            {
                IEnumerable<Teacher> ordered = teachers;
                if (question.LeftLimit.IsAvaliable)
                {
                    ordered = ordered.OrderBy(t =>
                    {
                        var mark = t.Marks.FirstOrDefault(m => m.QuestionId == question.Id);
                        if (mark != null)
                        {
                            float criteria = mark.Count > 10 ? 3 : mark.Count > 5 ? 2 : 1;
                            criteria /= (float)mark.Count;
                            return mark.Answer + criteria;
                        }
                        return 0f;
                    });

                    int count = GiveBadges(question, ordered, 'l');
                    ordered = ordered.Skip(count);
                }
                if (question.RightLimit.IsAvaliable)
                {
                    ordered = ordered.OrderByDescending(t =>
                    {
                        var mark = t.Marks.FirstOrDefault(m => m.QuestionId == question.Id);
                        if (mark != null)
                        {
                            float criteria = mark.Count > 10 ? 3 : mark.Count > 5 ? 2 : 1;
                            criteria /= (float)mark.Count;
                            return mark.Answer - criteria;
                        }
                        return 0f;
                    });

                    int count = GiveBadges(question, ordered, 'r');
                    ordered = ordered.Skip(count);
                }
                GiveBadges(question, ordered, badgesCount: int.MaxValue);
            }

            foreach (var teacher in teachers)
            {
                var avg = teacher.Marks.FirstOrDefault(mark => mark.QuestionId == Question.AvarageId);
                var difficult = teacher.Marks.FirstOrDefault(mark => mark.QuestionId == diffId);
                int maxCount = 0;

                if (avg != null)
                {
                    teacher.Marks.Remove(avg);
                    if (teacher.Marks.Count > 0)
                        maxCount = teacher.Marks.Max(m => m.Count);
                    var rate = new TeacherBadge() { Id = avg.QuestionId, Type = char.MaxValue };
                    if (difficult != null)
                    {
                        avg.Answer = (avg.Answer * (teacher.Marks.Count) - difficult.Answer) / (teacher.Marks.Count - 1);
                        rate.Mark = CalculateRate(difficult.Answer, avg.Answer, maxCount);
                    }
                    else
                    {
                        rate.Mark = -0.001f;
                    }
                    teacher.Badges.Insert(0, rate);
                }
                if(teacher.Marks.Count != 0)
                    teacher.VotesCount = teacher.Marks.Max(t => t.QuestionId == Question.AvarageId ? 0 : t.Count);
                Site.TeacherManager.Update(teacher);
            }

            return teachers;
        }

        protected void UpdateTeams(IList<Teacher> teachers = null)
        {
            if (teachers == null)
                teachers = Site.TeacherManager.All();

            var questions = Site.QuestionManager.All();
            var allVotes = Site.VoteManager.All();
            var teams = Site.TeamManager.All();

            foreach (var team in teams)
            {
                var filteredTeachers = teachers.AsEnumerable();
                if (team.Id != Team.AllTeachersTeamId)
                    filteredTeachers = filteredTeachers.Where(t => t.TeamId == team.Id);

                var teachersIds = filteredTeachers.ToDictionary(t => t.Id);
                UpdateTeam(allVotes.Where(m => teachersIds.ContainsKey(m.TeacherId)).GroupBy(m => m.QuestionId),
                    team,
                    questions.First().Id,
                    questions.Last().Id);

                Site.TeamManager.Update(team);
            }
        }

        protected void UpdateGroupStaytistic(IList<Vote> votes)
        {
            var studentVotes = votes.ToLookup(x => x.AccountId);
            var groups = Site.GroupManager.All();
            foreach (var group in groups)
            {
                var avgAnswers = new Dictionary<string, Mark>(7);
                var students = Site.StudentManager.GroupList(group.Id);
                var uniqueAccounts = 0;
                foreach (var student in students)
                {
                    if (student.AccountId.IsEmpty())
                        continue;

                    var accountVotes = studentVotes[student.AccountId];
                    if (accountVotes.Any())
                        uniqueAccounts++;

                    foreach (var vote in accountVotes)
                    {
                        var mark = avgAnswers.GetOrCreate(vote.QuestionId.Id);
                        mark.Count++;
                        mark.Answer += vote.Answer;
                        avgAnswers[vote.QuestionId.Id] = mark;
                    }
                }
                float avg = 0;
                foreach (var vote in avgAnswers.Values)
                    avg += (vote.Answer / (float)vote.Count);

                if (avgAnswers.Count == 0)
                {
                    group.VotesCount = 0;
                    group.Avg = 0;
                }
                else
                {
                    group.VotesCount = uniqueAccounts;
                    group.Avg = avg / avgAnswers.Count;
                }
                Site.GroupManager.Update(group);
            }
        }
        #endregion

        #region private
        private Dictionary<string, string> CreateResultsTags()
        {
            var result = new Dictionary<string, string>();
            result.Add("siteURL", "http://ztu-fikt.azurewebsites.net/");
            result.Add("siteName", "Evaluate");

            return result;
        }

        private void UpdateTeam(IEnumerable<IGrouping<Question, Vote>> votes, Team model, string difficultId, string additionalMarkId = null)
        {
            int maxCount = int.MinValue;
            float avgSum = 0;
            var dictionary = new Dictionary<string, Mark>();
            foreach (var group in votes)
            {
                var mark = dictionary.GetOrCreate(group.Key.Id);

                foreach (var vote in group)
                {
                    mark.Answer += vote.Answer;
                    mark.Count++;
                }
                mark.Answer /= mark.Count;

                avgSum += mark.Answer;
                if (mark.Count > maxCount)
                    maxCount = mark.Count;
            }

            model.AdditionalMark = dictionary.GetOrDefault(additionalMarkId);
            model.AvgDifficult = dictionary[difficultId].Answer;

            model.ClearRate = (avgSum - model.AvgDifficult) / (dictionary.Count - 1);
            model.Rate = new Mark()
            {
                Answer = CalculateRate(model.AvgDifficult, model.ClearRate, maxCount),
                Count = maxCount
            };
        }

        private int GiveBadges(Question question, IEnumerable<Teacher> teachers, char badgeType = char.MinValue, int badgesCount = 5)
        {
            int count = 0;
            float prevMark = 0;
            foreach (var teacher in teachers)
            {
                var mark = teacher.Marks.FirstOrDefault(m => m.QuestionId == question.Id);
                if (mark == null)
                {
                    prevMark = 0;
                    continue;
                }
                if (count >= badgesCount && prevMark != mark.Answer)
                    break;

                teacher.Badges.Add(new TeacherBadge() { Id = question.Id, Mark = mark.Answer, Type = badgeType });
                count++;
                prevMark = mark.Answer;
            }
            return count;
        }
        #endregion
    }
}
