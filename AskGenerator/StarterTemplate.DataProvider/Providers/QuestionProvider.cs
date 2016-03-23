using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider.Providers
{
    public class QuestionProvider : BaseEntityProvider<Question>, IQuestionProvider
    {
        public override List<Question> All()
        {
            return GetSet(set =>
            {
                return set.OrderBy(q => q.Order)
                    .ToList();
            });
        }
        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        public List<Question> List(bool isAboutTeacher)
        {
            return GetSet(set =>
            {
                return set.Where(q => q.IsAboutTeacher == isAboutTeacher)
                    .OrderBy(q => q.Order)
                    .ToList();
            });
        }
    }
}
