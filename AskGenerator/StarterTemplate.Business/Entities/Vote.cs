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
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "QuestionIdRequired")]
        [Display(Name = "QuestionId", ResourceType = typeof(Resources.Resource))]
        public Question QuestionId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "AnswerRequired")]
        [Display(Name = "Answer", ResourceType = typeof(Resources.Resource))]
        public short Answer { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "TeacherIdRequired")]
        [Display(Name = "TeacherId", ResourceType = typeof(Resources.Resource))]
        public string TeacherId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "AccountIdRequired")]
        [Display(Name = "AccountId", ResourceType = typeof(Resources.Resource))]
        public string AccountId { get; set; }
    }
}
