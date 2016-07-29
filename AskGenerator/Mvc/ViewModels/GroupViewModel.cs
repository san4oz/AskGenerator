using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Components.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class GroupViewModel : BaseViewModel, IMapFrom<Group>
    {
        [Display(Name = "Group", ResourceType=typeof(Resources.Resource))]
        [iRequired]
        public string Name { get; set; }

        [Display(Name = "Faculty", ResourceType = typeof(Resources.Resource))]
        [UIHint("FacultySelector")]
        public string FacultyId { get; set; }
    }
}
