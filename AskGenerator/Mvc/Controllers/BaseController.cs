using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.Components;
using AskGenerator.Mvc.ViewModels;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Controllers
{
    public class BaseController : Controller
    {
        private IQuestionManager QuestionManager { get; set; }

        public BaseController()
        {
            QuestionManager = Site.QuestionManager;
        }


        /// <summary>
        /// Gets or sets value indicating whether edit action is executing.
        /// It saves value in <see cref="ViewBag.IsEditing"/>.
        /// </summary>
        protected bool IsEditing
        {
            get
            {
                return ViewBag.IsEditing ?? false;
            }
            set
            {
                ViewBag.IsEditing = value; 
            }
        }

        /// <summary>
        /// Returns response with specified data and statuc code.
        /// </summary>
        /// <param name="data">The data to return.</param>
        /// <param name="code">Response status code.</param>
        /// <returns><see cref="T:JsonResult"/></returns>
        protected JsonResult Json(object data, int code)
        {
            Response.StatusCode = code;
            return Json(data);
        }

        /// <summary>
        /// Returns response with specified data and 404 statuc code.
        /// </summary>
        /// <param name="data">The data to return.</param>
        /// <returns><see cref="T:JsonResult"/></returns>
        protected JsonResult Json404(object data)
        {
            return Json(data, 404);
        }

        private RobotsInfo robots;
        /// <summary>
        /// Gets instance of <see cref="T:RobotsInfo"/> to configure page indexing.
        /// </summary>
        protected RobotsInfo Robots
        {
            get
            {
                return robots != null ? robots : robots = (RobotsInfo)ViewData.GetOrCreate("Robots", () => new RobotsInfo());
            }
        }

        private Resolver resolver;
        protected Resolver Resolver
        {
            get
            {
                if (resolver != null)
                    return resolver;
                else
                    return resolver = new Resolver();
            }
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="obj">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        protected TDestination Map<TDestination>(object obj)
        {
            return Mapper.Map<TDestination>(obj);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source list to a new destination list.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        protected IList<TDestination> MapList<TSource, TDestination>(IList<TSource> source)
        {
            return Mapper.Map<IList<TSource>, IList<TDestination>>(source);
        }

        /// <summary>
        /// Checks 'g-recaptcha-response' forms param and validate it. 
        /// </summary>
        /// <returns>Value indication whether validation completed with success</returns>
        protected async Task<bool> CheckCaptcha()
        {
            var captchaValues = Request.Form.GetValues("g-recaptcha-response");
            if (captchaValues == null || captchaValues.Length != 1)
                return false;

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["secret"] = "6LdmoRcTAAAAAMbnk_Je5Q_ZZmoXzk_V9j2jTRTV";
                values["response"] = captchaValues[0];
                values["remoteip"] = Request.UserHostAddress;

                var response = await client.UploadValuesTaskAsync("https://www.google.com/recaptcha/api/siteverify", values);
                var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CaptchaResponse>(Encoding.Default.GetString(response));

                return captchaResponse.success;
            }
        }

        /// <summary>
        /// The string to be used to format photo end-path.
        /// </summary>
        const string PhotoPathFormat = "/Content/Images/{0}{1}{2}";

        /// <summary>
        /// Saves image with specified id to the specified sub dirrectory.
        /// </summary>
        /// <param name="image">The image file.</param>
        /// <param name="id">Id of the photo.</param>
        /// <param name="subdir">The sub dirrectory save image to.</param>
        /// <returns>Path to the saved file.</returns>
        protected string SaveImage(HttpPostedFileBase image, string id, string subdir = null)
        {
            if (image == null || image.ContentLength <= 0)
                return null;

            if (!string.IsNullOrWhiteSpace(subdir))
                subdir = subdir.Trim('/') + '/';

            var path = PhotoPathFormat.FormatWith(subdir ?? string.Empty, id, Path.GetExtension(image.FileName));
            var serverPath = Server.MapPath(path);

            image.SaveAs(serverPath);
            return path;

        }

        /// <summary>
        /// Deletes file with specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">File path.</param>
        protected void DeleteFile(string path)
        {
            if (path.IsEmpty())
                return;

            var serverPath = Server.MapPath(path);
            if (System.IO.File.Exists(serverPath))
                System.IO.File.Delete(serverPath);
        }

        protected string TransformEmail(string email)
        {
            var t = email.Trim().Split('@');
            return t[0].Replace(".", string.Empty) + '@' + t[1].ToLower();
        }

        #region Badges
        protected Dictionary<string, LimitViewModel> CreateBadges(IList<Question> questions = null)
        {
            if (questions == null)
                questions = QuestionManager.All();

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

        /// <summary>
        /// Initializes <see cref="T:TeacherListViewModel"/> model. Returns questions used for creation.
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="model"></param>
        /// <returns>Returns questions used for creation.</returns>
        protected IList<Question> InitTeacherListViewModel(List<Teacher> teachers, TeacherListViewModel model)
        {
            var models = new List<TeacherViewModel>(teachers.Count);
            var questions = QuestionManager.All();
            var badges = CreateBadges(questions);

            foreach (var teacher in teachers)
            {
                var tmodel = Map<Teacher, TeacherViewModel>(teacher);

                tmodel.Badges.Each(b => b.Mark = (float)Math.Round(b.Mark, 2));
                models.Add(tmodel);
            }

            model.List = models.OrderByDescending(m => m.AverageMark != null ? m.AverageMark.Mark : -0.001f).ToList();
            model.Badges = badges;

            return questions;
        }

        protected float CalculateRate(float difficultAvg, float otherAvg, int votesCount)
        {
            return (float)(Math.Pow(difficultAvg, 0.5) * otherAvg * Math.Pow(votesCount, 0.2));
        }
        #endregion

        public class CaptchaResponse
        {
            public bool success { get; set; }
            public string[] error_codes { get; set; }
        }
    }
}
