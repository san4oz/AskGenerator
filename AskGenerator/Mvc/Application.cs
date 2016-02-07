using AskGenerator.App_Start;
using AskGenerator.App_Start.Autofac;
using AskGenerator.App_Start.AutoMapper;
using AskGenerator.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AskGenerator.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DBInitializer<AppContext>());
            ControllerBuilder.Current.DefaultNamespaces.Add("AskGenerator.Mvc.Controllers");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            AutoMapperConfig.Configure();
        }
    }

    internal class DBInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : System.Data.Entity.DbContext
    {

        public void InitializeDatabase(TContext context)
        {
            context.Database.CreateIfNotExists();
        }
    }
}
