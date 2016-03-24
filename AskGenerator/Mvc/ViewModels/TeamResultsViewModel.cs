using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class TeamResultsViewModel : TeacherListViewModel
    {
        public TeamResultsViewModel()
        {
            Avgs = new Dictionary<string, Mark>();
        }

        public string Id { get; set; }

        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        public IDictionary<string, IDictionary<short, int>> Marks { get; set; }

        public IDictionary<string, Mark> Avgs { get; set; }

        public Dictionary<string, string> Questions { get; set; }

        public IList<Team> Teams { get; set; }
    }
}
