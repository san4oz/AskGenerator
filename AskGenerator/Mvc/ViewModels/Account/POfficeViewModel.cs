using System;
using AskGenerator.App_Start.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AskGenerator.Business.Entities;

namespace AskGenerator.ViewModels
{
    using Mvc.Components.Attributes;
    using R = Resources.Resource;
    public class PrivateOfficeModel : UserModelBase
    {
        [iRequired]
        public override string Email { get; set; }

        [iRequired]
        public override string Password { get; set; }

        [iRequired]
        public override string PasswordConfirm { get; set; }
    }


    public class ResetPasswordModel
    {
        [iRequired]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "ValidLenght", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "ValidLenght", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public string Id { get; set; }
    }

    public class ForgotPasswordModel
    {
        [iRequired]
        [Display(Name = "Email", ResourceType = typeof(R))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "RegularAddress",
                                                                               ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "RegularAddress", ErrorMessageResourceType = typeof(R))]
        public string Email { get; set; }
    }
}

