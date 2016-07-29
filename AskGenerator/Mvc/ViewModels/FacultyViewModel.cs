using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    using Components.Attributes;
    using R = Resources.Resource;
    public class FacultyViewModel : BaseViewModel, IMapFrom<Faculty>
    {
        [Display(Name = "ShortName", ResourceType = typeof(R))]
        [StringLength(8, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(R))]
        [iRequired]
        public string ShortName { get; set; }

        [Display(Name = "FacultyName", ResourceType = typeof(R))]
        [iRequired]
        public string Name { get; set; }
    }
}
