using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class TeamResultsViewModel : TeacherListViewModel, IRateble
    {
        public TeamResultsViewModel()
        {
            Marks = new Dictionary<string, AnswerCountDictionary>();
        }

        public string Id { get; set; }

        public GeneralSettings.Iteration Iteration { get; set; }

        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        public IDictionary<string, AnswerCountDictionary> Marks { get; set; }

        public Mark Rating { get; set; }

        public Dictionary<string, string> Questions { get; set; }

        public IList<Team> Teams { get; set; }
    }

    public interface IRateble
    {
        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        IDictionary<string, AnswerCountDictionary> Marks { get; set; }

        Mark Rating { get; set; }
    }
}
