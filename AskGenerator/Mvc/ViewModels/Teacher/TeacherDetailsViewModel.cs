using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels.Teacher
{
    public class TeacherDetailsViewModel : TeacherViewModel
    {
        public Dictionary<string, string> Questions { get; set; }
    }
}
