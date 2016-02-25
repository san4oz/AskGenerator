using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Core
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

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified.
        /// </summary>
        /// <param name="name">The route name. Specify with underline: '_name' to add area prefix.</param>
        /// <param name="url">The URL to map.</param>
        /// <param name="defaults">Defaults values.</param>
        protected virtual void MapRoute(string name, string url, object defaults = null)
        {
            context.MapRoute(
               name[0] == '_' ? AreaName + name : name,
               AreaName.ToLower() + "/" + url,
               defaults ?? defaultsComponets,
               namespaces: namespaces
           );
        }
    }
}
