using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Modules
{
    public class UrlParamsHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.PreRequestHandlerExecute +=
                (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(Object source,
             EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            var mvcHandler = context.Handler != null ? context.Handler as MvcHandler : null;
            var routeData = mvcHandler != null ? mvcHandler.RequestContext.RouteData : null;

            if (routeData != null)
            {
                string returnUrlKey = null;
                foreach (var key in context.Request.QueryString.AllKeys)
                {
                    if (key.Equals("returnUrl", StringComparison.InvariantCultureIgnoreCase))
                    {
                        returnUrlKey = key;
                        break;
                    }
                }
                if (!returnUrlKey.IsEmpty())
                {
                    var url = context.Request.QueryString[returnUrlKey];
                    routeData.Values["returnUrl"] = url;
                }

                var iteration = new AskGenerator.Business.Entities.Settings.GeneralSettings.Iteration()
                {
                    Name = string.Empty
                };
                if (context.Request.QueryString.AllKeys.Contains("i"))
                {
                    var i = context.Request.QueryString["i"];
                    if (!i.IsEmpty())
                        iteration = Site.Settings.General().GetIteration(i);
                }
                routeData.Values["i"] = iteration;
            }
        }
    }
}
