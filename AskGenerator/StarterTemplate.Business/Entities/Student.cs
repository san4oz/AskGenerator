using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Business.Entities
{
    public class Student : Person
    {
        public Group Group { get; set; }

        public void Merge(Student student)
        {
            this.FirstName = student.FirstName.Or(this.FirstName);
            this.LastName = student.LastName.Or(this.LastName);
            this.Image = student.Image.Or(this.Image);
        }
    }
}
