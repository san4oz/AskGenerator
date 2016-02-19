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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Vote obj)
        {
            if (obj == null)
                return false;
            return string.Equals(TeacherId, obj.TeacherId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(AccountId, obj.AccountId, StringComparison.OrdinalIgnoreCase)
                && Answer == obj.Answer
                && obj.QuestionId.Id.Equals(QuestionId.Id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
