using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class VotingViewModel : BaseViewModel
    {
        public string QuestionId { get; set; }

        public string StudentId { get; set; }

        public string TeacherId { get; set; }

        public int Result { get; set; }
    }
}
