using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class CreateStudentViewModel
    {
        public StudentViewModel Student { get; set; }

        public IList<GroupViewModel> Groups { get; set; }
    }
}
