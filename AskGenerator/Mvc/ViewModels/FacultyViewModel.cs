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
    using R = Resources.Resource;
    public class FacultyViewModel : BaseViewModel, IMapFrom<Faculty>
    {

        [Display(Name = "ShortName", ResourceType = typeof(R))]
        [Required]
        public string ShortName { get; set; }

        [Display(Name = "FacultyName", ResourceType = typeof(R))]
        [Required]
        public string Name { get; set; }
    }
}
