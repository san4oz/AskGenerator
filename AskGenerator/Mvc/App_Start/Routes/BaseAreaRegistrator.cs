using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.App_Start.Routes
{
    public abstract class BaseAreaRegistrator : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext registrationContext)
        {
            this.context = registrationContext;
        }

        protected virtual object defaultsComponets { get { return new { action = "Index", id = UrlParameter.Optional }; } }

        protected virtual string[] namespaces { get { return new[] { "AskGenerator.Controllers" }; } }

        protected AreaRegistrationContext context;

        protected virtual void MapRoute(string name, string url, object defaults = null)
        {
            context.MapRoute(
               AreaName + "_" + name,
               AreaName.ToLower() + "/" + url,
               defaults ?? defaultsComponets,
               namespaces: namespaces
           );
        }
    }
}
