using AskGenerator.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Core;

namespace AskGenerator
{
    public static class Site
    {
        public static IStudentManager StudentManager { get { return Get<IStudentManager>(); } }

        public static ITeacherManager TeacherManager { get { return Get<ITeacherManager>(); } }

        public static IGroupManager GroupManager { get { return Get<IGroupManager>(); } }

        public static IQuestionManager QuestionManager { get { return Get<IQuestionManager>(); } }

        public static IPDFGenerator PDFGenerator { get { return Get<IPDFGenerator>(); } }

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}
