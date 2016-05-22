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
        public bool IsVotingEnabled
        {
            get
            {
                return Get<bool>(Keys.IsVotingEnabled);
            }
            set
            {
                Set(Keys.IsVotingEnabled, value);
            }
        }

        public string VotingDisableText
        {
            get
            {
                return Get<string>(Keys.VotingDisableText);
            }
            set
            {
                Set(Keys.VotingDisableText, value);
            }
        }

        public TimeBannerSettings TimeBanner
        {
            get
            {
                return Get<TimeBannerSettings>(Keys.TimeBanner) ?? new TimeBannerSettings();
            }
            set
            {
                Set(Keys.TimeBanner, value);
            }
        }

        public class Keys
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
                return "{0} {1} {2}:0:0 GMT+0{3}:00".FormatWith(Month, Day, Year, IsSammerTime ? "2" : "3");
            }
        }
    }
}
