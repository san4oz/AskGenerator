using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Controllers
{
    public class HomeController : BaseController
    {
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

        /// <summary>
        /// Gets data for voting page.
        /// </summary>
        /// <returns>Returns data for current user.</returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> NgData()
        {
            var userId = User.Identity.GetGroupId();
            var group = await Site.GroupManager.GetAsync(userId);
            var quesstions = await Site.QuestionManager.ListAsync(true);
            var votes = (await Site.VoteManager.ListAsync(userId)).GroupBy(v => v.TeacherId);
            var teachers = new List<TeacherDataModel>();
            foreach(var teacher in group.Teachers)
            {
                var data = new TeacherDataModel()
                {
                    id = teacher.Id,
                    image = teacher.Image,
                    name = teacher.FirstName + ' ' + teacher.LastName
                };
                var teacherVotes = votes.FirstOrDefault();
                if (teacherVotes == null)
                    data.status = new List<Answer>();
                else
                    data.status = teacherVotes.Select(v => new Answer() { id = v.Id, value = v.Answer }).ToList();

                teachers.Add(data);
            }
            ;
            return Json(new {
                options = quesstions.Select(q => new Option() { id = q.Id, label = q.QuestionBody }).ToList(),
                teachers = teachers
            });
        }

        public class TeacherDataModel
        {
            public string id { get; set; }

            public string name { get; set; }

            public string image { get; set; }

            public List<Answer> status { get; set; }
        }

        public class Answer
        {
            public string id { get; set; }

            public int value { get; set; }
        }

        public class Option
        {
            public string id { get; set; }

            public string label { get; set; }
        }
    }
}
