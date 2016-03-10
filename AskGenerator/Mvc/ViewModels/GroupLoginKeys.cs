using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels
{
    public class GroupLoginKeys : List<StudentKeyPair>
    {
        public GroupLoginKeys():base()
        {
        }

        public GroupLoginKeys(int capacity):base(capacity)
        {
        }

        public string GroupName { get; set; }
    }

    public class StudentKeyPair
    {
        public string Name { get; set; }

        public string Key { get; set; }
    }
}
