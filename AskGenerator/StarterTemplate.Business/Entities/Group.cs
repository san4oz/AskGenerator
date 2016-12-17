using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AskGenerator.Business.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities
{
    public class Group : Entity, IVersionedStatistics<Group.Statistics>
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ID of faculty this group related to.
        /// </summary>
        [ForeignKey("Faculty")]
        public string FacultyId { get; set; }

        public Faculty Faculty { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        [NotMapped]
        public float AverageVote { get; set; }

        /// <summary>
        /// The number of voted students.
        /// </summary>
        [NotMapped]
        public int StudentsCount { get; set; }

        /// <summary>
        /// Answer-count pairs per question ID.
        /// </summary>
        [NotMapped]
        public Dictionary<string, AnswerCountDictionary> Marks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public Mark Rating { get; set; }

        /// <summary>
        /// Returns a string name that represents the current group.
        /// </summary>
        /// <returns><see cref="Name"/> of the group.</returns>
        public override string ToString()
        {
            return Name;
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
            if (this.Marks == null)
                this.Marks = new Dictionary<string, AnswerCountDictionary>(0);

            stat.AverageVote = this.AverageVote;
            stat.StudentsCount = this.StudentsCount;
            stat.Marks = new SerializableDictionary<string, AnswerCountDictionary>(this.Marks);
            stat.Rating = Rating;
        }

        #region IVersionedStatistics members
        public const string Prefix = "group";

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
            AverageVote = stat.AverageVote;
            StudentsCount = stat.StudentsCount;
            Marks = stat.Marks ?? new Dictionary<string, AnswerCountDictionary>();
            Rating = stat.Rating ?? new Mark();
        }
        #endregion

        [XmlType("Stat")]
        public class Statistics : RatableGroupsStatistic
        {
            [XmlElement("Avg")]
            public float AverageVote { get; set; }

            [XmlElement("Count")]
            public int StudentsCount { get; set; }
        }
    }
}
