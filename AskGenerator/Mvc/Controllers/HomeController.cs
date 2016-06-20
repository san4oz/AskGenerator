using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Settings;
using AskGenerator.Business.Filters;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.Components.Attributes;
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

        #region culture
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
        #endregion

        #region Voting
        
        [HttpGet]
        [WebsiteAuthorize(WebsiteSettings.Keys.IsVotingEnabled)]
        [ActionName("Index")]
        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Vote()
        {
            if (!Site.Settings.Website().IsVotingEnabled)
                return View("VotingDisabledNow");

            return View();
        }

        
        [ActionName("view")]
        [OutputCache(CacheProfile = "Cache1Hour")]
        public PartialViewResult NgView()
        {
            return PartialView();
        }

        /// <summary>
        /// Gets data for voting page.
        /// </summary>
        /// <returns>Returns data for current user.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NgData()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account", new { returnUrl = Url.Action("Index") }) }, 403);
            }
            var userId = User.Identity.GetId();
            var groupId = User.Identity.GetGroupId();
            var group = await Site.GroupManager.GetAsync(groupId);
            var quesstions = await Site.QuestionManager.AllAsync();
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
        [WebsiteAuthorize(WebsiteSettings.Keys.IsVotingEnabled)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddAnswer(string id, string questionId, short answer)
        {
            if (!Site.Settings.Website().IsVotingEnabled)
                return Json(false, 403);
            var vote = new Vote()
            {
                TeacherId = id,
                Answer = answer,
                AccountId = User.Identity.GetId()
            };
            var result = await Site.VoteManager.Save(vote, questionId);
            if (result)
                return Json(true);

            return Json(false, 505);
        }

        #endregion

        #region Board
        [HttpGet]
        [OutputCache(CacheProfile="Cache1Hour")]
        public async Task<ActionResult> Board(string i = "")
        {
            var iteration = i.IsEmpty() ? null : Site.Settings.General().GetIteration(i);
            if (!i.IsEmpty() && iteration == null)
                return HttpNotFound("Iteration {0} was not found".FormatWith(i));

            var questions = await Site.QuestionManager.AllAsync();
            var badges = await Task.Factory.StartNew<Dictionary<string, LimitViewModel>>(() => CreateBadges(questions));
            var questionsDictionary = questions.ToDictionary(q => q.Id, q => q.QuestionBody);

            var model = new BoardViewModel(questionsDictionary, badges);
            model.UniqueUsers = Site.VoteManager.UniqueUserCount();
            if (iteration != null)
                model.Iteration = iteration;
            return View(model);
        }

        [HttpPost]
        [OutputCache(Duration = CacheDuration * 60, VaryByParam = "i", Location = OutputCacheLocation.Server)]
        [ActionName("Board")]
        public async Task<ActionResult> BoardPost(string i = "")
        {
            var iteration = i.IsEmpty() ? null : Site.Settings.General().GetIteration(i);
            if (!i.IsEmpty() && iteration == null)
                return HttpNotFound("Iteration {0} was not found".FormatWith(i));

            var manager = Site.TeacherManager;
            var teachers = await Site.TeacherManager.AllAsync(false);
            if (iteration != null)
            {
                manager.LoadHistory(teachers);
                teachers.AsParallel().ForAll(t => t.InitStatistics(iteration.Id));
            }

            var model = new TeacherListViewModel();
            InitTeacherListViewModel(teachers, model);
            return View("_Board", model);
        }
        #endregion

        #region Details
        public ActionResult TeacherResults(string id)
        {
            return View();
        }
        #endregion

        #region Subscribe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Subscribe(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                subscriber.Email = TransformEmail(subscriber.Email);
                Site.Subscribers.Create(subscriber);
                return Json(subscriber.Email);
            }
            return Json(false, 500);
        }
        #endregion

        #region protected
        protected List<TeacherDataModel> MapTeachers(Business.Entities.Group group, IEnumerable<IGrouping<string, Business.Entities.Vote>> votes)
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
        #endregion

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