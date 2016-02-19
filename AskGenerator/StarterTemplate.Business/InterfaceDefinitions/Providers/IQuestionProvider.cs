using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface IQuestionProvider : IBaseEntityProvider<Question>
    {
        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        List<Question> List(bool isAboutTeacher);
    }
}
