using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class TeacherViewModel : BaseViewModel, IMapFrom<Teacher>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Group> Groups { get; set; }

        public List<string> SelectedGroups { get; set; }
    }
}
