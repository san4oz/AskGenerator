using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class TeacherVotingViewModel
    {
        public TeacherViewModel Teacher { get; set; }

        public List<StudentViewModel> Students { get; set; }

        public VotingViewModel Vote { get; set; }
    }
}
