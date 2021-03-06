﻿using AskGenerator.Mvc.Components;
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

        protected JsonResult Json(object data, int code)
        {
            Response.StatusCode = code;
            return Json(data);
        }

        protected JsonResult Json404(object data)
        {
            return Json(data, 404);
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

            var path = string.Format("/Content/Images/{0}{1}{2}", subdir ?? string.Empty, id, Path.GetExtension(image.FileName));
            var serverPath = Server.MapPath(path);

            image.SaveAs(serverPath);
            return path;

        }

        /// <summary>
        /// Deletes file with specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">File path.</param>
        protected void DeleteImage(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            var serverPath = Server.MapPath(path);
            if (System.IO.File.Exists(serverPath))
                System.IO.File.Delete(serverPath);
        }

        protected string TransformEmail(string email)
        {
            var t = email.ToLower().Split('@');
            return t[0].Replace(".", string.Empty) + '@' + t[1];
        }

        public class CaptchaResponse
        {
            public bool success { get; set; }
            public string[] error_codes { get; set; }
        }
    }
}
