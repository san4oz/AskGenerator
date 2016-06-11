using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities.Settings
{
    [NotMapped]
    public class WebsiteSettings : Settings
    {
        public override string Id
        {
            get
            {
                return "WebsiteSettings";
            }
        }

        public bool IsVotingEnabled { get; set; }

        public string VotingDisabledText { get; set; }

        public TimeBannerSettings TimeBanner { get; set; }

        /// <summary>
        /// Initializes fields from XML fields.
        /// </summary>
        public override void Initialize()
        {
            IsVotingEnabled = Get<bool>(Keys.IsVotingEnabled);
            VotingDisabledText = Get<string>(Keys.VotingDisableText);
            TimeBanner = Get<TimeBannerSettings>(Keys.TimeBanner) ?? new TimeBannerSettings();
        }

        /// <summary>
        /// Save fields to XML fields.
        /// </summary>
        public override void Apply()
        {
            Set(Keys.IsVotingEnabled, IsVotingEnabled);
            Set(Keys.VotingDisableText, VotingDisabledText);
            Set(Keys.TimeBanner, TimeBanner);
        }

        public static class Keys
        {
            public const string IsVotingEnabled = "IsVoting";
            public const string VotingDisableText = "VotingDisTxt";
            public const string TimeBanner = "TimeBanner";
        }

        public class TimeBannerSettings
        {
            public bool Enabled { get; set; }

            public bool IsSammerTime { get; set; }            

            public string Header { get; set; }

            public string Month { get; set; }

            public short Day { get; set; }

            public short Hour { get; set; }

            public short Year { get; set; }

            /// <summary>
            /// Renders timer date in 'March 23 2016 20:0:0 GMT+02:00' format.
            /// </summary>
            /// <returns>String cotains timer date.</returns>
            public override string ToString()
            {
                return "{0} {1} {2} {3}:0:0 GMT+0{4}:00".FormatWith(Day, Month, Year, Hour, IsSammerTime ? "2" : "3");
            }
        }
    }
}
