using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Settings;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Mvc.Controllers;
using AskGenerator.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AskGenerator.Mvc.ViewModels.Settings;

namespace AskGenerator.Controllers.Admin
{
    [Authorize(Roles = Role.Admin)]
    public class SettingController : BaseController
    {
        protected ISettingManager Manager { get; private set; }

        public SettingController()
        {
            Manager = Site.Settings;
        }
        
        [HttpGet]
        public ActionResult Website()
        {
            var model = Map<WebsiteSettings, WebsiteSettingsModel>(Manager.Website(true));
            return View(model); 
        }

        [HttpPost]
        public ActionResult Website(WebsiteSettingsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var setting = Map<WebsiteSettingsModel, WebsiteSettings>(model);
            Manager.Update(setting);

            return RedirectToAction("Website");
        }

        [HttpGet]
        public ActionResult General()
        {
            var model = Map<GeneralSettings, GeneralSettingsModel>(Manager.General(true));
            return View(model);
        }

        [HttpPost]
        public ActionResult General(GeneralSettingsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var setting = Map<GeneralSettingsModel, GeneralSettings>(model);
            Manager.Update(setting);

            return RedirectToAction("General");
        }
    }
}
