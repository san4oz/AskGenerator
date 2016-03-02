using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "IsPersistent", ResourceType = typeof(Resources.Resource))]
        public bool IsPersistent { get; set; }
    }
}
