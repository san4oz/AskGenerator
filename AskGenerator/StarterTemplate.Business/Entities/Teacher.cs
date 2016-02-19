using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Teacher : Person
    {
        public virtual ICollection<Group> Groups { get; set; }

        public string TeamId { get; set; }

        [NotMapped]
        public IList<Mark> Marks { get; set; }

        public Teacher()
        {
            Marks = new List<Mark>();
        }
    }
}
