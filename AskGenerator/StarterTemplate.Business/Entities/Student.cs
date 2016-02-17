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
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "GroupRequired")]
        [Display(Name = "Group", ResourceType = typeof(Resources.Resource))]
        public Group Group { get; set; }
    }
}
