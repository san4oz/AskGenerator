using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using AskGenerator.Mvc;
using AskGenerator.Business.InterfaceDefinitions;
using AskGenerator.DataProvider;
using AskGenerator.DataProvider.Providers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using AskGenerator.Business.Managers;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Core;
using AskGenerator.Generator;

namespace AskGenerator.App_Start.Autofac
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Application).Assembly);

            RegisterTypes(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            //Entity providers section
            builder.RegisterType<StudentProvider>().As<IStudentProvider>();
            builder.RegisterType<TeacherProvider>().As<ITeacherProvider>();
            builder.RegisterType<GroupProvider>().As<IGroupProvider>();
            builder.RegisterType<QuestionProvider>().As<IQuestionProvider>();
            builder.RegisterType<VoteProvider>().As<IVoteProvider>();
            builder.RegisterType<TeamProvider>().As<ITeamProvider>();

            //Entity managers section
            builder.RegisterType<StudentManager>().As<IStudentManager>();
            builder.RegisterType<TeacherManager>().As<ITeacherManager>();
            builder.RegisterType<GroupManager>().As<IGroupManager>();
            builder.RegisterType<QuestionManager>().As<IQuestionManager>();
            builder.RegisterType<VoteManager>().As<IVoteManager>();
            builder.RegisterType<TeamManager>().As<ITeamManager>();

            //Core
            builder.RegisterType<PDFRocket>().As<IPDFGenerator>();
        }
    }
}
