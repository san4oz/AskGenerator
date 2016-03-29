using AskGenerator.App_Start;
using AskGenerator.App_Start.Autofac;
using AskGenerator.App_Start.AutoMapper;
using AskGenerator.App_Start.Routes;
using AskGenerator.DataProvider;
using AskGenerator.Core.Binders;
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
            SetModelBinders();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void OnBefore(object sender, EventArgs e)
        {
            var x = 1 + 1;
            Console.WriteLine(x);
        }

        private void SetModelBinders()
        {
            var floarBinder = new FloatBinder();
            ModelBinders.Binders.Add(typeof(decimal), floarBinder);
            ModelBinders.Binders.Add(typeof(decimal?), floarBinder);

            ModelBinders.Binders.Add(typeof(float), floarBinder);
            ModelBinders.Binders.Add(typeof(float?), floarBinder);

            ModelBinders.Binders.Add(typeof(double), floarBinder);
            ModelBinders.Binders.Add(typeof(double?), floarBinder);
        }
    }
}
