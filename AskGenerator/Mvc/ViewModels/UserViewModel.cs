using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    using System.Web.Mvc;
using R = Resources.Resource;
    public class UserViewModel : BaseViewModel, IMapFrom<User>
    {
        [HiddenInput(DisplayValue=true)]
        public override string Id { get; set; }
        
        [Display(Name = "Email", ResourceType = typeof(R))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "RegularAddress",
                                                                               ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "RegularAddress", ErrorMessageResourceType = typeof(R))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(R), ErrorMessageResourceName = "Required")]
        [Display(Name = "Group", ResourceType = typeof(R))]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        
        public string StudentId { get; set; }
        
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "ValidLenght", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RepitPassword", ResourceType = typeof(R))]
        [Compare("Password", ErrorMessageResourceName = "NoRepitPassword", ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public bool EmailConfirmed {get; set;}

        
        [Required(ErrorMessageResourceType = typeof(R), ErrorMessageResourceName = "Required")]
        [StringLength(7, MinimumLength = 7, ErrorMessageResourceName = "ExactLenghtError", ErrorMessageResourceType = typeof(R))]
        public string LoginKey {get; set;}
    }
}
