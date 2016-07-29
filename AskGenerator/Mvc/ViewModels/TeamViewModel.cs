using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.Mvc.Components.Attributes;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class TeamViewModel : BaseViewModel, IMapFrom<Team>
    {
        [Display(Name = "TeamName", ResourceType = typeof(Resources.Resource))]
        [iRequired]
        public string Name { get; set; }
    }
}
