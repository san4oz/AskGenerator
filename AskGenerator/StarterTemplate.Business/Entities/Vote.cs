using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Business.Entities
{
    public class Vote : Entity
    {
        public Question QuestionId { get; set; }

        public short Answer { get; set; }

        public string TeacherId { get; set; }

        public string AccountId { get; set; }
    }
}
