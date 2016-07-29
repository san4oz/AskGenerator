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
    using Mvc.Components.Attributes;
    using R = Resources.Resource;
    public class RegistrationModel : UserModelBase
    {
        [ScaffoldColumn(false)]
        public override string Id { get; set; }

        [iRequired]
        public override string Email { get; set; }

        [iRequired]
        [Display(Name = "Group", ResourceType = typeof(R))]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [iRequired]
        [Display(Name = "LastName", ResourceType = typeof(R))]
        [System.Web.Mvc.Remote("CheckLastName", "Account", AdditionalFields = "GroupId",
            ErrorMessageResourceName = "NoLastNameFound", ErrorMessageResourceType = typeof(R))]
        public string LastName { get; set; }

        [iRequired]
        public override string Password { get; set; }

        [iRequired]
        public override string PasswordConfirm { get; set; }
    }
}
