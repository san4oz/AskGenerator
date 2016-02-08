using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Controllers
{
    public class BaseController : Controller
    {
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

        public class CaptchaResponse
        {
            public bool success { get; set; }
            public string[] error_codes { get; set; }
        }
    }
}
