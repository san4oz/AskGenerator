using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Components.Attributes
{
    public class WebsiteAuthorizeAttribute : AuthorizeAttribute
    {
        string settingName;

        public WebsiteAuthorizeAttribute() : base()
        {
            Order = 0; 
        }

        public WebsiteAuthorizeAttribute(string settingName) :this()
        {
            this.settingName = settingName;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!settingName.IsEmpty())
            {
                var settings = Site.Settings.Website();
                if (!settings.Get<bool>(settingName))
                    return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}
