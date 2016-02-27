using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Parsers
{
    public class StudentTextParser : BaseParser
    {
        #region Managers
        protected IGroupManager GroupManager { get; private set; }
        protected IStudentManager StudentManager { get; private set; }
        #endregion

        /// <summary>
        /// Dictionary of groups accessable by <see cref="Group.Name"/>
        /// </summary>
        protected IDictionary<string, Group> Groups { get; private set; }

        #region Columns
        protected Column Index = new Column("INDEX");
        protected Column FirstName = new Column("FIRSTNAME");
        protected Column SecondName = new Column("SECONDNAME");
        protected Column FatherName = new Column("FATHERNAME");
        protected Column Group = new Column("GROUP");
        protected Column Course = new Column("COURSE");

        protected readonly IList<Column> Columns;
        #endregion

        public StudentTextParser(IGroupManager groupManager, IStudentManager studentManager)
        {
            GroupManager = groupManager;
            StudentManager = studentManager;
            Columns = new List<Column>() { Index, FirstName, SecondName, FatherName, Group, Course };
        }

        /// <summary>
        /// Parse students text file asynchronously.
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
            if (Group.IsInitialized)
            {
                HandleHeadLine(line);
            }
            else
            {
                var student = new Student()
                {
                    FirstName = line.Col(FirstName),
                    IsMale = true,
                    LastName = line.Col(SecondName)
                };
                student.Group = GetGroup(line);
                var fatherName = line.Col(FatherName);
                if (!fatherName.IsEmpty())
                    student.LastName += ' ' + fatherName;
                StudentManager.Create(student);
            }
        }

        protected Entities.Group GetGroup(IList<string> line)
        {
            var groupName = prepareGroupName(line.Col(Group));
            var group = Groups.GetOrDefault(groupName);
            if (group == null)
            {
                group = new Group() { Name = groupName };
                GroupManager.Create(group);
            }
            return group;
        }

        protected virtual void HandleHeadLine(IEnumerable<string> line)
        {
            byte i = 0;
            foreach (var col in line)
            {
                var column = Columns.SingleOrDefault(c => c.Key == col.ToUpperInvariant());
                if (column != null)
                    column.Index = i;
                else
                    column.NotExists();

                i++;
            }
        }

        #region private
        private string prepareGroupName(string groupName)
        {
            var letter = groupName.Last();
            if (char.IsLetter(letter))
                groupName = groupName.Substring(0, groupName.Length - 1).ToUpperInvariant() + char.ToUpperInvariant(letter);
            else
                groupName = groupName.ToUpperInvariant();
            return groupName;
        }
        #endregion
    }
}
