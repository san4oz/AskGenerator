using AskGenerator.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities
{
    public class Teacher : Person, IVersionedStatistics<Teacher.Statistics>
    {
        public virtual ICollection<Group> Groups { get; set; }

        public string TeamId { get; set; }

        [NotMapped]
        public List<TeacherBadge> Badges { get; set; }

        [NotMapped]
        public int VotesCount { get; set; }

        [NotMapped]
        public IList<TeacherQuestion> Marks { get; set; }

        public Teacher()
        {
            Marks = new List<TeacherQuestion>();
        }

        /// <summary>
        /// Initializes fields from XML fields.
        /// </summary>
        public override void Initialize()
        {
            var stat = Fields.GetOrDefault<Statistics>("Statistics", () => new Statistics());
            InitStatiscics(stat);
        }

        /// <summary>
        /// Save fields to XML fields.
        /// </summary>
        public override void Apply()
        {
            var stat = Fields.GetOrCreate<Statistics>("Statistics", () => new Statistics());

            stat.Badges = Badges;
            stat.VotesCount = VotesCount;
        }

        #region IVersionedStatistics members
        public string HistoryPrefix
        {
            get { return "teach"; }
        }

        [NotMapped]
        public Dictionary<int, Statistics> Versions { get; set; }

        public bool InitStatistics(int iterationID)
        {
            var stat = Versions.GetOrDefault(iterationID);
            if (stat == null)
                return false;

            InitStatiscics(stat);
            return true;
        }

        protected void InitStatiscics(Statistics stat)
        {
            Badges = stat.Badges ?? new List<TeacherBadge>();
            VotesCount = stat.VotesCount;
        }
        #endregion

        [XmlType("Stat")]
        public class Statistics
        {
            [XmlElement("Bads")]
            public List<TeacherBadge> Badges { get; set; }

            [XmlElement("Votes")]
            public int VotesCount { get; set; }
        }
    }

    [XmlType("Badge")]
    public class TeacherBadge
    {
        public string Id { get; set; }

        [XmlElement("M")]
        public float Mark { get; set; }

        /// <summary>
        /// 'r' - right, 'l' - left, <see cref="char.MinValue"/> - not for display
        /// </summary>
        [XmlElement("T")]
        public char Type { get; set; }

        public TeacherBadge()
        {
            Type = char.MinValue;
        }
    }
}