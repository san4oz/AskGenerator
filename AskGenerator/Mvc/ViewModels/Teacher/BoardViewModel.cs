using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class BoardViewModel
    {
        public BoardViewModel(Dictionary<string, string> questions = null, Dictionary<string, LimitViewModel> badges = null)
        {
            Questions = questions;
            Badges = badges ?? new Dictionary<string, LimitViewModel>();
        }

        public Dictionary<string, LimitViewModel> Badges { get; set; }
        public Dictionary<string, string> Questions { get; set; }
    }
}
