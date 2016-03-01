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
    using R = Resources.Resource;
    public class RegistrationModel : BaseViewModel, IMapFrom<User>
    {
        [ScaffoldColumn(false)]
        public override string Id { get; set; }

        [Required]
        [Display(Name = "Електронна адреса")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Група")]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(R))]
        [System.Web.Mvc.Remote("CheckLastName", "Account", AdditionalFields = "GroupId",
            ErrorMessageResourceName = "NoLastNameFound", ErrorMessageResourceType = typeof(R))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
