using AskGenerator.DataProvider;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Data.Entity;
[assembly: OwinStartup(typeof(AskGenerator.App_Start.Startup))]
namespace AskGenerator.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            if (AppContext.Connection.StartsWith("test", System.StringComparison.OrdinalIgnoreCase))
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
            app.CreatePerOwinContext<AppContext>(AppContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
