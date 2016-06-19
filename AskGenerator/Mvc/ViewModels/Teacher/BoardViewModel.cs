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
            Iteration = new Business.Entities.Settings.GeneralSettings.Iteration();
        }

        public Dictionary<string, LimitViewModel> Badges { get; set; }

        public Dictionary<string, string> Questions { get; set; }

        public int UniqueUsers { get; set; }

        public AskGenerator.Business.Entities.Settings.GeneralSettings.Iteration Iteration { get; set; }
    }
}
