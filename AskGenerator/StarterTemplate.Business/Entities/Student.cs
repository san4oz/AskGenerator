using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskGenerator.Business.Entities
{
    public class Student : Person
    {
        public Group Group { get; set; }

        /// <summary>
        /// Indicates whether student has registred account.
        /// </summary>
        public bool HasUserAccount { get; set; }

        [MaxLength(128)]
        public string AccountId { get; set; }

        public void Merge(Student student)
        {
            this.FirstName = student.FirstName.Or(this.FirstName);
            this.LastName = student.LastName.Or(this.LastName);
            this.Image = student.Image.Or(this.Image);
        }
    }
}
