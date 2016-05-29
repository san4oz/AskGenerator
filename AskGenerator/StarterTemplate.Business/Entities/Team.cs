using AskGenerator.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public float ClearRate { get; set; }

        /// <summary>
        /// Represents additional mark. It may be overall impressions.
        /// </summary>
        [NotMapped]
        public Mark AdditionalMark { get; set; }

        [NotMapped]
        public Mark Rate { get; set; }

        public override void Initialize()
        {
            var stat = Fields.GetOrDefault<Statistics>("Statistics", () => new Statistics());
            InitStatiscics(stat);
        }

        public override void Apply()
        {
            var stat = Fields.GetOrCreate<Statistics>("Statistics", () => new Statistics());

            stat.AvgDifficult = AvgDifficult;
            stat.ClearRate = ClearRate;
            stat.AdditionalMark = AdditionalMark;
            stat.Rate = Rate;

            Fields["Statistics"] = stat;
        }

        #region IVersionedStatistics members
        public string HistoryPrefix
        {
            get { return "team"; }
        }

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
            ClearRate = stat.ClearRate;
            AdditionalMark = stat.AdditionalMark ?? new Mark();
            Rate = stat.Rate ?? new Mark();
        }
        #endregion

        public class Statistics
        {
            public float AvgDifficult { get; set; }

            /// <summary>
            /// Represents average mark without difficult and votes count.
            /// </summary>
            public float ClearRate { get; set; }

            /// <summary>
            /// Represents additional mark. It may be overall impressions.
            /// </summary>
            public Mark AdditionalMark { get; set; }

            public Mark Rate { get; set; }
        }
    }
}
