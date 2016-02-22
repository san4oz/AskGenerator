using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class TeacherListViewModel
    {
        public TeacherListViewModel(IList<TeacherViewModel> list = null, Dictionary<string, Badge> badges = null)
        {
            List = list ?? new List<TeacherViewModel>();
            Badges = badges ?? new Dictionary<string, Badge>();
        }

        public IList<TeacherViewModel> List { get; set; }

        public Dictionary<string, Badge> Badges { get; set; }
    }
}
