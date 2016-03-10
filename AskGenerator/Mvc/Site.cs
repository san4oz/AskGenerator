using AskGenerator.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Core;
using AskGenerator.DataProvider;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AskGenerator
{
    public static class Site
    {
        public static IStudentManager StudentManager { get { return Get<IStudentManager>(); } }

        public static ITeacherManager TeacherManager { get { return Get<ITeacherManager>(); } }

        public static IGroupManager GroupManager { get { return Get<IGroupManager>(); } }

        public static IQuestionManager QuestionManager { get { return Get<IQuestionManager>(); } }

        public static IVoteManager VoteManager { get { return Get<IVoteManager>(); } }

        public static ITeamManager TeamManager { get { return Get<ITeamManager>(); } }

        public static ITeacherQuestionManager TQManager { get { return Get<ITeacherQuestionManager>(); } }

        public static UserManager UserManager { get { return HttpContext.Current.GetOwinContext().GetUserManager<UserManager>(); } }

        public static RoleManager RoleManager { get { return HttpContext.Current.GetOwinContext().GetUserManager<RoleManager>(); } }

        public static IPDFGenerator PDFGenerator { get { return Get<IPDFGenerator>(); } }

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}
