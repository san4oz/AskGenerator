using AskGenerator.App_Start;
using AskGenerator.App_Start.Autofac;
using AskGenerator.App_Start.AutoMapper;
using AskGenerator.App_Start.Routes;
using AskGenerator.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AskGenerator.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppContext>());
            ControllerBuilder.Current.DefaultNamespaces.Add("AskGenerator.Mvc.Controllers");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            AutoMapperConfig.Configure();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
