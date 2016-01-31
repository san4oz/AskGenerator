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

        public MultiSelectList AllGroups { get; set; }

        public TeacherComposeViewModel()
        {
            var groups = Site.GroupManager.All();
            AllGroups = new MultiSelectList(groups, "Id", "Name");
        }
    }
}
