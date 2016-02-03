using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AskGenerator.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "student/list",
                url: "student/list",
                defaults: new { controller = "Student", action = "List" }
            );

            routes.MapRoute(
              name: "student/create",
              url: "student/create",
              defaults: new { controller = "Student", action = "Create" }
          );

            routes.MapRoute(
                name: "teacher/list",
                url: "teacher/list",
                defaults: new { controller = "Teacher", action = "list" }
            );

            routes.MapRoute(
               name: "teacher/students",
               url: "teacher/{teacherId}/students",
               defaults: new { controller = "Teacher", action = "Students" }
           );

            routes.MapRoute(
                name: "teacher/pdf",
                url: "teacher/pdf",
                defaults: new { controller = "Teacher", action = "GeneratePDF" }
            );

            routes.MapRoute(
                name: "teacher/create",
                url: "teacher/create",
                defaults: new { controller = "Teacher", action = "create" }
            );

            routes.MapRoute(
                name: "group/list",
                url: "group/list",
                defaults: new { controller = "Group", action = "list" }
            );

            routes.MapRoute(
                name: "group/create",
                url: "group/create",
                defaults: new { controller = "Group", action = "create" }
            );

            routes.MapRoute(
               name: "question/create",
               url: "question/create",
               defaults: new { controller = "Question", action = "Create" }
           );

            routes.MapRoute(
              name: "question/list",
              url: "question/list",
              defaults: new { controller = "Question", action = "List" }
          );

         
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
