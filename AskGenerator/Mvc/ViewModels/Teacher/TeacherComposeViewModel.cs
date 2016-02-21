using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.ViewModels
{
    public class TeacherComposeViewModel
    {
        public TeacherViewModel Teacher { get; set; }

        public IList<SelectListItem> AllGroups { get; set; }

        public IList<SelectListItem> Teams { get; set; }

        public TeacherComposeViewModel()
        {
        }

        public TeacherComposeViewModel(TeacherViewModel teacher)
        {
            Teacher = teacher ?? new TeacherViewModel();
            bool isSelected = true;
            if (Teacher.Groups == null || Teacher.Groups.Count == 0)
                isSelected = false;
            var groups = Site.GroupManager.All();
            var teams = Site.TeamManager.All();
            AllGroups = groups
                .Select(g => new SelectListItem() { Text = g.Name, Value = g.Id, Selected = isSelected && Teacher.Groups.Any(s => s.Id.Equals(g.Id)) })
                .ToList();

            Teams = teams
                .Select(t => new SelectListItem() { Text = t.Name, Value = t.Id, Selected = t.Id.Equals(Teacher.TeamId) })
                .ToList();
            Teams.Insert(0, new SelectListItem() {Value=" ", Text=" ----- "});
        }
    }
}
