using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Parsers
{
    public class StudentTextParser : BaseParser
    {
        protected IGroupManager GroupManager { get; private set; }
        protected IStudentManager StudentManager { get; private set; }

#warning Check spelling
        /// <summary>
        /// Dictionary of groups accesseble by <see cref="Group.Name"/>
        /// </summary>
        protected IDictionary<string, Group> Groups { get; private set; }
        public StudentTextParser(IGroupManager groupManager, IStudentManager studentManager)
        {
            GroupManager = groupManager;
            StudentManager = studentManager;
        }

#warning Check spelling
        /// <summary>
        /// Parse students text file asynchroniously.
        /// </summary>
        /// <param name="path"></param>
        public override async void ParseText(string path)
        {
            var groups = await GroupManager.AllAsync();
            Groups = groups.ToDictionary(g => g.Name);
            base.ParseText(path, HandleLine);
        }

        protected virtual void HandleLine(IList<string> line)
        {
            var groupName = line.Last().ToUpperInvariant();
        }
    }
}
