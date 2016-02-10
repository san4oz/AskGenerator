using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    class Vote : Entity
    {

        public string QuestionId { get; set; }

        public int Answer { get; set; }

        public string PersonId { get; set; }

    }
}
