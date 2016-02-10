using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Question : Entity
    {
        public string QuestionBody { get; set; }

        public string LowerRateDescription { get; set; }

        public string HigherRateDescription { get; set; }

        public bool IsAboutTeacher { get; set; }

    }
}
