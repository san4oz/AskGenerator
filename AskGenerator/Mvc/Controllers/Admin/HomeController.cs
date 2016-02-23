using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    public class HomeController : Controller
    {    
        [OutputCache(CacheProfile="Cache1Hour")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
