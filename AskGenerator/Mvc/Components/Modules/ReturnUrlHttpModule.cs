using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Modules
{
    public class ReturnUrlHttpModule : IHttpModule
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
                ((MvcHandler)context.Handler).RequestContext.RouteData.Values["returnUrl"] = url;
            }
        }
    }
}
