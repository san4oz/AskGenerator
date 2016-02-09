using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class QuestionController : BaseController
    {
        [HttpGet]
        public ActionResult List()
        {
            var models = Site.QuestionManager.All();
            var viewModels = Map<IList<Question>, IList<QuestionViewModel>>(models);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModel viewModel)
        {
            var model = Map<QuestionViewModel, Question>(viewModel);
            Site.QuestionManager.Create(model);
            return RedirectToAction("List");
        }
    }
}
