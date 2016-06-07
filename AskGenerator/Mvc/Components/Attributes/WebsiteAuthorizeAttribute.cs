using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Components.Attributes
{
    /// <summary>
    /// Represents an attribute that is used to restrict access by callers to an action method and could be skipped according to the settings.
    /// </summary>
    public class WebsiteAuthorizeAttribute : AuthorizeAttribute
    {
        string settingName;

        public WebsiteAuthorizeAttribute() : base()
        {
            Order = 0; 
        }

        /// <summary>
        /// Initilizes new instance with skipping setting name.
        /// </summary>
        /// <param name="settingName">Setting name from website settings.</param>
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
