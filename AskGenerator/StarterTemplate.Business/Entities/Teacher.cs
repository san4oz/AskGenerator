using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
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
        public List<TeacherBadge> Badges { get; set; }

        [NotMapped]
        public IList<TeacherQuestion> Marks { get; set; }

        public Teacher()
        {
            Marks = new List<TeacherQuestion>();
        }

        /// <summary>
        /// Initializes field from XML
        /// </summary>
        public void Initilize()
        {
            Badges = Fields.GetOrCreate<List<TeacherBadge>>("Badges");
        }
    }

    public class TeacherBadge
    {
        public string Id { get; set; }

        public float Mark { get; set; }

        /// <summary>
        /// 'r' - right, 'l' - left, <see cref="char.MinValue"/> - not for display
        /// </summary>
        public char Type { get; set; }

        public TeacherBadge()
        {
            Type = char.MinValue;
        }
    }
}