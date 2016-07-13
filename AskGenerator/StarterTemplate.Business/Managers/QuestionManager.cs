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
        protected override string Name { get { return "Question"; } }

        public QuestionManager(IQuestionProvider provider)
            : base(provider)
        { }
    }
}
