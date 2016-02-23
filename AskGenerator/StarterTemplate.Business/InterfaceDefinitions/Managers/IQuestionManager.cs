using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IQuestionManager : IBaseManager<Question>
    {
        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        List<Question> List(bool isAboutTeacher = false);

        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        Task<List<Question>> ListAsync(bool isAboutTeacher = false);

        Task<Dictionary<string, Badge>> CreateBadgesAsync();

        Dictionary<string, Badge> CreateBadges();
    }
}
