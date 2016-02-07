using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers
{
    public class GroupController : BaseController
    {
        [HttpGet]
        public ActionResult List( )
        {
            var groups = Site.GroupManager.All();
            var viewModel = Map<IList<Group>, IList<GroupViewModel>>(groups);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create( )
        {
            var group = new GroupViewModel();
            return View(group);
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel viewModel)
        {
            var group = Map<GroupViewModel, Group>(viewModel);
            Site.GroupManager.Create(group);
            return RedirectToAction("List");
        }
    }
}
