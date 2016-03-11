using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    using R = Resources.Resource;
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Email", ResourceType = typeof(R))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "RegularAddress",
                                                                               ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "RegularAddress", ErrorMessageResourceType = typeof(R))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "ValidLenght", ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(7, MinimumLength = 7, ErrorMessageResourceName = "ExactLenghtError", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Key", ResourceType = typeof(R))]
        public string Key { get; set; }

        [Display(Name = "IsPersistent", ResourceType = typeof(R))]
        public bool IsPersistent { get; set; }
    }
}
