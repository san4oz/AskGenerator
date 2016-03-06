using AskGenerator.Business.Entities;
using AskGenerator.Business.Filters;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AskGenerator.Mvc.Controllers
{
    [Culture]
    public class HomeController : BaseController
    {
        /// <summary>
        /// Cache duration for board page in seconds.
        /// </summary>
        public const int CacheDuration = 60;

        protected IQuestionManager QuestionManager { get; set; }

        public HomeController()
        {
            QuestionManager = Site.QuestionManager;
        }

        #region Voting
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("view")]
        public PartialViewResult NgView()
        {
            return PartialView();
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "de" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }


        /// <summary>
        /// Gets data for voting page.
        /// </summary>
        /// <returns>Returns data for current user.</returns>
        [HttpPost]
        public async Task<JsonResult> NgData()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account") }, 403);
            }
            var userId = User.Identity.GetGroupId();
            var group = await Site.GroupManager.GetAsync(userId);
            var quesstions = await Site.QuestionManager.ListAsync(true);
            var votes = await Site.VoteManager.ListAsync(userId);
            var teachers = MapTeachers(group, votes.GroupBy(v => v.TeacherId));

            return Json(new
            {
                options = quesstions.Select(q => new Option()
                {
                    id = q.Id,
                    label = q.QuestionBody,
                    low = q.LowerRateDescription,
                    hight = q.HigherRateDescription,
                }).ToList(),
                teachers = teachers
            });
        }

        /// <summary>
        /// Saves answer for current user.
        /// </summary>
        /// <param name="id">Teacher ID.</param>
        /// <param name="questionId">Question ID.</param>
        /// <param name="answer">Answer.</param>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddAnswer(string id, string questionId, short answer)
        {
            var vote = new Vote()
            {
                TeacherId = id,
                Answer = answer,
                AccountId = User.Identity.GetGroupId()
            };
            var result = await Site.VoteManager.Save(vote, questionId);
            if (result)
                return Json(true);

            return Json(false, 505);
        }
        #endregion

        [HttpGet]
        [OutputCache(Duration = CacheDuration * 60, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public async Task<ViewResult> Board()
        {
            var badges = await Task.Factory.StartNew<Dictionary<string, LimitViewModel>>(CreateBadges);
            var questions = Site.QuestionManager.List(true).ToDictionary(q => q.Id, q => q.QuestionBody);
            var model = new BoardViewModel(questions, badges);
            return View(model);
        }

        [HttpPost]
        [OutputCache(Duration = CacheDuration * 60, VaryByParam = "none", Location = OutputCacheLocation.Any)]
        [ActionName("Board")]
        public async Task<ViewResult> BoardPost()
        {
            var teachers = await Site.TeacherManager.AllAsync(true);
            var model = CreateTeacherListViewModel(teachers);
            return View("_Board", model);
        }

        protected Dictionary<string, LimitViewModel> CreateBadges()
        {
            var questions = QuestionManager.List(true);
            var result = new Dictionary<string, LimitViewModel>(questions.Count * 2);
            foreach (var question in questions)
            {
                if (question.LeftLimit.AvgLimit > 0)
                {
                    var badge = Map<Question.Limit, LimitViewModel>(question.LeftLimit);
                    badge.Id = question.Id;
                    result.Add(badge.Id + "l", badge);
                }
                if (question.RightLimit.AvgLimit > 0)
                {
                    var badge = Map<Question.Limit, LimitViewModel>(question.RightLimit);
                    badge.Id = question.Id;
                    result.Add(badge.Id + "r", badge);
                }
            }
            return result;
        }

        protected TeacherListViewModel CreateTeacherListViewModel(List<Teacher> teachers)
        {
            var badges = CreateBadges();
            var models = new List<TeacherViewModel>(teachers.Count);
            foreach (var teacher in teachers)
            {
                var tmodel = Map<Teacher, TeacherViewModel>(teacher);
                var avg = teacher.Marks.FirstOrDefault(mark => mark.QuestionId == Question.AvarageId);
                if (avg != null)
                {
                    tmodel.AverageMark = avg.Answer;
                    teacher.Marks.Remove(avg);
                }
                foreach (var mark in teacher.Marks)
                {
                    var id = mark.QuestionId + 'l';
                    var badge = badges.GetOrDefault(id);
                    if (badge != null && badge.AvgLimit > mark.Answer)
                        tmodel.Badges.Add(new TeacherBadge() { Id = id, Mark = mark.Answer });

                    id = mark.QuestionId + 'r';
                    badge = badges.GetOrDefault(id);
                    if (badge != null && badge.AvgLimit < mark.Answer)
                        tmodel.Badges.Add(new TeacherBadge() { Id = id, Mark = mark.Answer });
                }
                models.Add(tmodel);
            }
            var model = new TeacherListViewModel(models.OrderByDescending(m => m.AverageMark).ToList(), badges);
            return model;
        }

        private List<TeacherDataModel> MapTeachers(Business.Entities.Group group, IEnumerable<IGrouping<string, Business.Entities.Vote>> votes)
        {
            var teachers = new List<TeacherDataModel>();
            foreach (var teacher in group.Teachers)
            {
                var data = new TeacherDataModel()
                {
                    id = teacher.Id,
                    image = string.IsNullOrEmpty(teacher.Image) ? "/Content/Images/teacher/noImage.png" : teacher.Image,
                    name = teacher.LastName + ' ' + teacher.FirstName
                };
                var teacherVotes = votes.FirstOrDefault(g => g.Key == teacher.Id);
                if (teacherVotes == null)
                    data.status = new List<Answer>();
                else
                    data.status = teacherVotes.Select(v => new Answer() { id = v.QuestionId.Id, value = v.Answer }).ToList();

                data.image = Resolver.Image(teacher.Image, teacher.IsMale);
                teachers.Add(data);
            }
            return teachers;
        }

        #region Nested
        public class TeacherDataModel
        {
            public string id { get; set; }

            public string name { get; set; }

            public string image { get; set; }

            public List<Answer> status { get; set; }
        }

        public class Answer
        {
            /// <summary>
            /// Question ID.
            /// </summary>
            public string id { get; set; }

            public int value { get; set; }
        }

        public class Option
        {
            public string id { get; set; }

            public string label { get; set; }

            public string low { get; set; }

            public string hight { get; set; }
        }
        #endregion
    }
}
