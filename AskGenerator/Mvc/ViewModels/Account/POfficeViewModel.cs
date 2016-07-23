using System;
using AskGenerator.App_Start.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AskGenerator.ViewModels
{
    using Business.Entities;
    using R = Resources.Resource;
    public class PrivateOfficeViewModel : BaseViewModel, IMapFrom<Student>
    {
        [Display(Name = "FirstName", ResourceType = typeof(R))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(R))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Group", ResourceType = typeof(R))]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [Display(Name = "Group", ResourceType = typeof(R))]
        public GroupViewModel Group { get; set; }


        /// <summary>
        /// Indicates whether student has registred account.
        /// </summary>
        public bool HasUserAccount { get; set; }

        [MaxLength(128)]
        public string AccountId { get; set; }

        [Display(Name = "Student", ResourceType = typeof(R))]
        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
    public class ExternalLoginConfirmationModel
        {
            [Required]
            [Display(Name = "Адрес электронной почты")]
            public string Email { get; set; }
        }

        public class ExternalLoginListModel
        {
            public string ReturnUrl { get; set; }
        }

        public class SendCodeViewModel
        {
            public string SelectedProvider { get; set; }

            public ICollection<SelectListItem> Providers { get; set; }

            public string ReturnUrl { get; set; }

            public bool RememberMe { get; set; }
        }

        public class VerifyCodeModel
        {
            [Required]
            public string Provider { get; set; }

            [Required]
            [Display(Name = "Код")]
            public string Code { get; set; }
            public string ReturnUrl { get; set; }

            [Display(Name = "Запомнить браузер?")]
            public bool RememberBrowser { get; set; }

            public bool RememberMe { get; set; }
        }

        public class ForgotModel
        {
            [Required]
            [Display(Name = "Адрес электронной почты")]
            public string Email { get; set; }
        }


        public class ResetPasswordModel
        {

            [Required]
            [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтверждение пароля")]
            [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }

            public string Id { get; set; }
        }

        public class ForgotPasswordViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Почта")]
            public string Email { get; set; }
        }
    }

