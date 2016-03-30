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
        #region politics
        /// <summary>
        /// An index action to return site politics.
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
        /// An index action to return site politics.
        /// </summary>
        /// <returns>Partial view.</returns>
        [OutputCache(CacheProfile = "Cache1HourAll")]
        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            return PartialView();
        }
        #endregion

        #region calculation
        /// <summary>
        /// An action to return calculation rules.
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
        /// An action to return calculation rules.
        /// </summary>
        /// <returns>Partial view.</returns>
        [OutputCache(CacheProfile = "Cache1HourAll")]
        [HttpPost]
        [ActionName("Calculation")]
        public ActionResult CalculationPost()
        {
            return PartialView();
        }
        #endregion

        #region badging
        /// <summary>
        /// An action to return badging rules.
        /// </summary>
        /// <returns>ViewResult or PartialViewResult.</returns>
        [OutputCache(CacheProfile = "Cache1Hour")]
        [HttpGet]
        public ActionResult Badging()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        /// <summary>
        /// An action to return badging rules.
        /// </summary>
        /// <returns>Partial view.</returns>
        [OutputCache(CacheProfile = "Cache1HourAll")]
        [HttpPost]
        [ActionName("Badging")]
        public ActionResult BadgingPost()
        {
            return PartialView();
        }
        #endregion
    }
}
