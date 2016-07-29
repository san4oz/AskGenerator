using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AskGenerator.App_Start.Routes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                name: "Board",
                url: "",
                defaults: new { controller = "Home", action = "Board" }
            );

            routes.MapRoute(
                name: "Vote",
                url: "vote",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Registration",
                url: "registration",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "PrivateOffice",
                url: "profile",
                defaults: new { controller = "Account", action = "PrivateOffice" }
            );

            routes.MapRoute(
                name: "ForgotPassword",
                url: "profile/forgot",
                defaults: new { controller = "Account", action = "ForgotPassword" }
            );

            routes.MapRoute(
                name: "ResetPassword",
                url: "profile/reset",
                defaults: new { controller = "Account", action = "ResetPassword" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
