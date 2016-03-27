using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.ViewModels
{
    public class GroupStatisticViewModel : BaseViewModel, IRateble
    {

        public IList<Group> Groups { get; set; }

        /// <summary>
        /// Answer - count pairs per question ID.
        /// </summary>
        public IDictionary<string, TeamResultsViewModel.AnswerCountDictionary> Marks { get; set; }

        public Mark Rate { get; set; }

        public Dictionary<string, string> Questions { get; set; }

        public GroupStatisticViewModel()
        {
            Marks = new Dictionary<string, TeamResultsViewModel.AnswerCountDictionary>();
        }
    }
}
