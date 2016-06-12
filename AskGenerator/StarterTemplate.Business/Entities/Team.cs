using AskGenerator.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities
{
    public class Team : Entity, IVersionedStatistics<Team.Statistics>
    {
        public string Name { get; set; }

        public const string AllTeachersTeamId = "all";

        [NotMapped]
        public float AvgDifficult { get; set; }

        /// <summary>
        /// Represents average mark without difficult and votes count.
        /// </summary>
        [NotMapped]
        public float ClearRating { get; set; }

        /// <summary>
        /// Represents additional mark. It may be overall impressions.
        /// </summary>
        [NotMapped]
        public Mark AdditionalMark { get; set; }

        [NotMapped]
        public Mark Rating { get; set; }

        /// <summary>
        /// Answer-count pairs per question ID.
        /// </summary>
        [NotMapped]
        public Dictionary<string, AnswerCountDictionary> Marks { get; set; }

        public override void Initialize()
        {
            var stat = Fields.GetOrDefault<Statistics>("Statistics", () => new Statistics());
            InitStatiscics(stat);
        }

        public override void Apply()
        {
            var stat = Fields.GetOrCreate<Statistics>("Statistics", () => new Statistics());

            stat.AvgDifficult = AvgDifficult;
            stat.ClearRating = ClearRating;
            stat.AdditionalMark = AdditionalMark;
            stat.Rating = Rating;
            stat.Marks = new SerializableDictionary<string, AnswerCountDictionary>(Marks);

            Fields["Statistics"] = stat;
        }

        #region IVersionedStatistics members
        public string HistoryPrefix
        {
            get { return "team"; }
        }

        [NotMapped]
        public Dictionary<int, Team.Statistics> Versions { get; set; }

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
            AvgDifficult = stat.AvgDifficult;
            ClearRating = stat.ClearRating;
            AdditionalMark = stat.AdditionalMark ?? new Mark();
            Rating = stat.Rating ?? new Mark();
            Marks = stat.Marks ?? new Dictionary<string, AnswerCountDictionary>();
        }
        #endregion

        [XmlType("Stat")]
        public class Statistics : RatableGroupsStatistic
        {
            [XmlElement("Avg")]
            public float AvgDifficult { get; set; }

            /// <summary>
            /// Represents average mark without difficult and votes count.
            /// </summary>
            [XmlElement("Cr")]
            public float ClearRating { get; set; }

            /// <summary>
            /// Represents additional mark. It may be overall impressions.
            /// </summary>
            [XmlElement("AddMark")]
            public Mark AdditionalMark { get; set; }

            public override Mark Rating { get; set; }
        }
    }
}
