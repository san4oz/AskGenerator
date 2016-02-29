using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class QuestionManager : BaseEntityManager<Question, IQuestionProvider>, IQuestionManager
    {
        public QuestionManager(IQuestionProvider provider)
            : base(provider)
        { }

        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        public List<Question> List(bool isAboutTeacher = false)
        {
            return Provider.List(isAboutTeacher);
        }

        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        public Task<List<Question>> ListAsync(bool isAboutTeacher = false)
        {
            return Task.Factory.StartNew(() => Provider.List(isAboutTeacher));
        }
    }
}
