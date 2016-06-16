using AskGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.App_Start.Routes
{
    public class AdminAreaRegistrator : BaseAreaRegistrator
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }
        protected override string[] namespaces { get { return new[] { "AskGenerator.Controllers.Admin" }; } }
        protected override object defaultsComponets { get { return new { controller = "Home", action = "Index", id = UrlParameter.Optional }; } }

        public override void RegisterArea(AreaRegistrationContext registrationContext)
        {
            base.RegisterArea(registrationContext);

            MapRoute(
                name: "student/list",
                url: "student/list",
                defaults: new { controller = "Student", action = "List" }
            );

            MapRoute(
              name: "student/create",
              url: "student/create",
              defaults: new { controller = "Student", action = "Create" }
          );

            MapRoute(
             name: "student/edit",
             url: "student/edit",
             defaults: new { controller = "Student", action = "Edit" }
         );

            MapRoute(
            name: "student/delete",
            url: "student/delete",
            defaults: new { controller = "Student", action = "Delete" }
        );

            MapRoute(
                name: "teacher/list",
                url: "teacher/list",
                defaults: new { controller = "Teacher", action = "list" }
            );
            
            MapRoute(
                name: "teacher/create",
                url: "teacher/create",
                defaults: new { controller = "Teacher", action = "create" }
            );

            MapRoute(
               name: "teacher/edit",
               url: "teacher/edit/{id}",
               defaults: new { controller = "Teacher", action = "Edit" }
           );

            MapRoute(
                name: "group/list",
                url: "group/list",
                defaults: new { controller = "Group", action = "list" }
            );

            MapRoute(
                name: "group/create",
                url: "group/create",
                defaults: new { controller = "Group", action = "create" }
            );

            MapRoute(
               name: "question/create",
               url: "question/create",
               defaults: new { controller = "Question", action = "Create" }
           );

            MapRoute(
               name: "question/edit",
               url: "question/edit/{id}",
               defaults: new { controller = "Question", action = "Edit" }
           );

            MapRoute(
             name: "question/delete",
             url: "question/delete/{id}",
             defaults: new { controller = "Question", action = "Delete" }
         );

            MapRoute(
              name: "question/list",
              url: "question/list",
              defaults: new { controller = "Question", action = "List" }
          );

            MapRoute(
               name: "questionAT/create",
               url: "question/teacher/create",
               defaults: new { controller = "Question", action = "CreateTeacher" }
           );

            MapRoute(
              name: "questionAT/list",
              url: "question/teacher/list",
              defaults: new { controller = "Question", action = "TeacherList" }
          );

            MapRoute(
                name: "_Config",
                url: "config/{action}",
                defaults: new { controller = "Setting", action = "Website" }
            );

            MapRoute(
                name: "_Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
