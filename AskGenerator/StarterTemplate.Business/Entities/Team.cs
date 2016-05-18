using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Team : Entity
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
            AvgDifficult = Fields.GetOrDefault<float>("AvgDifficult");
            ClearRate = Fields.GetOrDefault<float>("ClearRate");
            Func<Mark> createMark = () => new Mark();
            AdditionalMark = Fields.GetOrDefault<Mark>("AdditionalMark", createMark);
            Rate = Fields.GetOrDefault<Mark>("Rate", createMark);
        }

        public override void Apply()
        {
            Fields["AvgDifficult"] = AvgDifficult;
            Fields["ClearRate"] = ClearRate;
            Fields["AdditionalMark"] = AdditionalMark;
            Fields["Rate"] = Rate;
        }
    }
}
