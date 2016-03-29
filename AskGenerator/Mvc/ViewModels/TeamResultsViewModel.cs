using AskGenerator.Business.Entities;
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

        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        public IDictionary<string, AnswerCountDictionary> Marks { get; set; }

        public Mark Rate { get; set; }

        public Dictionary<string, string> Questions { get; set; }

        public IList<Team> Teams { get; set; }

        /// <summary>
        /// Keys - answers, values - counts.
        /// </summary>
        [Serializable]
        public class AnswerCountDictionary : Dictionary<short, int>
        {
            public AnswerCountDictionary()
                : base()
            {
                Avg = new Mark();
            }

            public AnswerCountDictionary(int capacity)
                : base(capacity)
            { }

            public Mark Avg { get; set; }

            public double D()
            {
                double result = 0;
                int totalCount = 0;
                foreach (var key in Keys)
                {
                    var count = this[key];
                    result = result + Math.Pow(key, 2) * count;
                    totalCount += count;
                }
                result /= (double)totalCount;
                return result - Math.Pow(Avg.Answer, 2);
            }
        }
    }

    public interface IRateble
    {
        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        IDictionary<string, TeamResultsViewModel.AnswerCountDictionary> Marks { get; set; }

        Mark Rate { get; set; }
    }
}
