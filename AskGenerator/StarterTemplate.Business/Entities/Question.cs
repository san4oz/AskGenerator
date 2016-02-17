using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Business.Entities
{
    public class Question : Entity
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "QuestionBodyRequired")]
        [Display(Name = "QuestionBody", ResourceType = typeof(Resources.Resource))]
        public string QuestionBody { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "LowerRateDescriptionRequired")]
        [Display(Name = "LowerRateDescription", ResourceType = typeof(Resources.Resource))]
        public string LowerRateDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "HigherRateDescriptionRequired")]
        [Display(Name = "HigherRateDescription", ResourceType = typeof(Resources.Resource))]
        public string HigherRateDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "IsAboutTeacheRequired")]
        [Display(Name = "IsAboutTeache", ResourceType = typeof(Resources.Resource))]
        public bool IsAboutTeacher { get; set; }

    }
}
