using System.ComponentModel.DataAnnotations;

namespace AskGenerator.ViewModels
{
    using Mvc.Components.Attributes;
    using R = Resources.Resource;
    public class UserViewModel : UserModelBase
    {
        [Display(Name = "Group", ResourceType = typeof(R))]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [Display(Name = "Faculty", ResourceType = typeof(R))]
        [UIHint("FacultySelector")]
        public string FacultyId { get; set; }
        
        public string StudentId { get; set; }

        public bool EmailConfirmed {get; set;}
 
        [iRequired]
        [StringLength(7, MinimumLength = 7, ErrorMessageResourceName = "ExactLenghtError", ErrorMessageResourceType = typeof(R))]
        public string LoginKey {get; set;}
    }
}
