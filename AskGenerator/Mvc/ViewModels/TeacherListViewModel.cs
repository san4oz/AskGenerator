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
        public TeacherListViewModel(IList<TeacherViewModel> list = null)
        {
            List = list ?? new List<TeacherViewModel>();
        }
        public IList<TeacherViewModel> List { get; set; }
    }
}
