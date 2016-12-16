using System.Web.Mvc;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using AskGenerator.Business.Managers;

namespace AskGenerator
{
    public static class Site
    {
        public static IStudentManager StudentManager { get { return Get<IStudentManager>(); } }

        public static ISettingManager Settings { get { return Get<ISettingManager>(); } }

        public static ITeacherManager TeacherManager { get { return Get<ITeacherManager>(); } }

        public static IGroupManager GroupManager { get { return Get<IGroupManager>(); } }

        public static IQuestionManager QuestionManager { get { return Get<IQuestionManager>(); } }

        public static IVoteManager VoteManager { get { return Get<IVoteManager>(); } }

        public static ITeamManager TeamManager { get { return Get<ITeamManager>(); } }

        public static IFacultyManager FacultyManager { get { return Get<IFacultyManager>(); } }

        public static ITeacherQuestionManager TQManager { get { return Get<ITeacherQuestionManager>(); } }

        public static IBaseEntityManager<Subscriber> Subscribers { get { return Get<IBaseEntityManager<Subscriber>>(); } }

        public static CacheManager Cache { get { return Get<CacheManager>(); } }

        public static UserManagerBase UserManager { get { return HttpContext.Current.GetOwinContext().GetUserManager<UserManagerBase>(); } }

        public static RoleManagerBase RoleManager { get { return HttpContext.Current.GetOwinContext().GetUserManager<RoleManagerBase>(); } }

        #region private
        private static T Get<T>()
        {
            var key = typeof(T).Name;
            if (HttpContext.Current.Items.Contains(key))
                return (T)(HttpContext.Current.Items[typeof(T).Name]);

            var t = DependencyResolver.Current.GetService<T>();
            HttpContext.Current.Items[typeof(T).Name] = t;
            return t;
        }
        #endregion
    }
}
