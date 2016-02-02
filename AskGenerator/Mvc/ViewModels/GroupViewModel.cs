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
    public class GroupViewModel : BaseViewModel, IMapFrom<Group>
    {
        [Display(Name="Группа")]
        public string Name { get; set; }

        public List<Student> Students { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}
