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
        /// <summary>
        /// Gets all questions ordered by <see cref="Question.Order"/>.
        /// </summary>
        /// <returns>List of retrived questions.</returns>
        public override List<Question> All()
        {
            return GetSet(set =>
            {
                return set.OrderBy(q => q.Order)
                    .ToList();
            });
        }
    }
}
