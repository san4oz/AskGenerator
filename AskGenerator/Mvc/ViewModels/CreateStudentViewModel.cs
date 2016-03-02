using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.ViewModels
{
    using R = Resources.Resource;
    public class CreateStudentViewModel
    {
        [Required(ErrorMessageResourceType = typeof(R),
                 ErrorMessageResourceName = "Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessageResourceType = typeof(R),
                 ErrorMessageResourceName = "Length")]
        [Display(Name = "Student", ResourceType = typeof(R))]
        public StudentViewModel Student { get; set; }

        [Required(ErrorMessageResourceType = typeof(R),
         ErrorMessageResourceName = "Required")]

        [Display(Name = "Group", ResourceType = typeof(R))]
        public IList<GroupViewModel> Groups { get; set; }
    }
}
