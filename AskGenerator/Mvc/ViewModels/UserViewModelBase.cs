using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.ViewModels
{
    using R = Resources.Resource;
    public class UserModelBase : BaseViewModel, IMapFrom<User>
    {
        [HiddenInput(DisplayValue=true)]
        public override string Id { get; set; }
        
        [Display(Name = "Email", ResourceType = typeof(R))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "RegularAddress",
                                                                               ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "RegularAddress", ErrorMessageResourceType = typeof(R))]
        public virtual string Email { get; set; }
        
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "ValidLenght", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "Password", ResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "RepitPassword", ResourceType = typeof(R))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "NoRepitPassword", ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.Password)]
        public virtual string PasswordConfirm { get; set; }
    }
}
