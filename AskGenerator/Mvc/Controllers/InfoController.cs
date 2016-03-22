using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace AskGenerator.Mvc.Controllers
{
    public class InfoController : BaseController
    {
        /// <summary>
        /// Index action to return site politics.
        /// </summary>
        /// <returns>ViewResult or PartialViewResult.</returns>
        [OutputCache(CacheProfile = "Cache1Hour")]
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        /// <summary>
        /// Index action to return site politics.
        /// </summary>
        /// <returns>ViewResult or PartialViewResult.</returns>
        [OutputCache(CacheProfile = "Cache1Hour", Location = OutputCacheLocation.Any)]
        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        /// <summary>
        /// Action to return calculation rules.
        /// </summary>
        /// <returns>ViewResult or PartialViewResult.</returns>
        [OutputCache(CacheProfile = "Cache1Hour")]
        [HttpGet]
        public ActionResult Calculation()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        /// <summary>
        /// Action to return calculation rules.
        /// </summary>
        /// <returns>ViewResult or PartialViewResult.</returns>
        [OutputCache(CacheProfile = "Cache1Hour", Location = OutputCacheLocation.Any)]
        [HttpPost]
        [ActionName("Calculation")]
        public ActionResult CalculationPost()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
    }
}
