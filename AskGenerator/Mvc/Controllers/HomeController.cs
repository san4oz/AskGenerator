using AskGenerator.Business.Entities;
using AskGenerator.Business.Filters;
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

namespace AskGenerator.Mvc.Controllers
{
    [Culture]
    public class HomeController : BaseController
    {
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
                    hight = q.HigherRateDescription
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
        public async Task<ViewResult> Board()
        {
            var teachers = await Site.TeacherManager.All(true);
            var model = new TeacherListViewModel(MapList<Teacher, TeacherViewModel>(teachers));
            return View(model);
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
                    name = teacher.FirstName + ' ' + teacher.LastName
                };
                var teacherVotes = votes.FirstOrDefault(g => g.Key == teacher.Id);
                if (teacherVotes == null)
                    data.status = new List<Answer>();
                else
                    data.status = teacherVotes.Select(v => new Answer() { id = v.QuestionId.Id, value = v.Answer }).ToList();

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
