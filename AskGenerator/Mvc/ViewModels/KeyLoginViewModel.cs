using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    using R = Resources.Resource;
    public class KeyLoginViewModel
    {
        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessageResourceName = "ExactLenghtError", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Key", ResourceType = typeof(R))]
        public string Key { get; set; }
    }
}
