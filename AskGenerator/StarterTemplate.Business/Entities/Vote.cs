using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Vote : Entity
    {
        public Question Question { get; set; }

        public short Answer { get; set; }

        public Teacher Teacher { get; set; }

        public User Account { get; set; }
    }
}
