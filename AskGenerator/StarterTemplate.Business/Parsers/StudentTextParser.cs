using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using AskGenerator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Business.Parsers
{
    /// <summary>
    /// Parser to parse text with students and save them.
    /// </summary>
    /// <remarks>
    /// Use following text format:
    /// index		secondname		firstname		fathername		group		course
    /// 10		Криворот		Людмила		Володимирівна		СІ-68В		4
    /// 20		Тищенко		Сергій		*		СІ-68В		4
    /// 'Group' and 'firstname' columns are required.
    /// Columns possitions can be changed.
    /// To skip using some column use '*', ' ', or '' cell.
    /// </remarks>
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
        /// Parse students stream.
        /// </summary>
        /// <param name="path"></param>
        public void ParseStream(Stream stream)
        {
            using (this.Info = new ParseInfo())
            {
                InitGroups();
                base.ParseText(stream, HandleLine);
            }
        }

        /// <summary>
        /// Parse students text file.
        /// </summary>
        /// <param name="path"></param>
        public override void ParseText(string path)
        {
            using (this.Info = new ParseInfo())
            {
                InitGroups();
                base.ParseText(path, HandleLine);
            }
        }

        protected virtual void HandleLine(IList<string> line)
        {
            if (!Group.IsInitialized || !Group.Exists || !FirstName.IsInitialized || !FirstName.Exists)
            {
                HandleHeadLine(line);
            }
            else
            {
                HandleSudentLine(line);
            }
        }

        private void HandleSudentLine(IList<string> line)
        {
            var student = new Student()
            {
                FirstName = line.Col(FirstName),
                IsMale = true,
                LastName = line.Col(SecondName)
            };
            student.Group = GetGroup(line);
            if (student.Group == null)
            {
                this.Info.Scipped.Add(line.Join("  "));
                return;
            }

            var fatherName = line.Col(FatherName);
            if (!fatherName.IsEmpty() && fatherName != "*")
                student.FirstName += ' ' + fatherName;
            if (StudentManager.MergeOrCreate(student))
                this.Info.New++;
        }

        protected Entities.Group GetGroup(IList<string> line)
        {
            var groupName = line.Col(Group);
            if (groupName.IsEmpty())
                return null;
            groupName = prepareGroupName(groupName);

            var group = Groups.GetOrDefault(groupName);
            if (group == null)
            {
                group = new Group() { Name = groupName };
                Groups.Add(group.Name, group);
                GroupManager.Create(group);
            }
            return group;
        }

        protected virtual void HandleHeadLine(IEnumerable<string> line)
        {
            byte i = 0;
            foreach (var col in line)
            {
                var key = col.ToUpperInvariant();
                var column = Columns.SingleOrDefault(c => c.Key == key);
                if (column != null)
                    column.Index = i;
                else
                    column.NotExists();

                i++;
            }
        }

        #region private
        protected virtual string prepareGroupName(string groupName)
        {
            var letter = groupName.Last();
            if (char.IsLetter(letter))
                groupName = groupName.Substring(0, groupName.Length - 1).ToUpperInvariant() + char.ToLowerInvariant(letter);
            else
                groupName = groupName.ToUpperInvariant();
            return groupName;
        }

        private void InitGroups()
        {
            var groups = GroupManager.Provider.AllWithoutIncl();

            Groups = new Dictionary<string, Group>();
            foreach (var g in groups)
                Groups[g.Name] = g;
        }
        #endregion
    }
}
