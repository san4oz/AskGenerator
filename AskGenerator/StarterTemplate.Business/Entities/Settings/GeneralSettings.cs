using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities.Settings
{
    /// <summary>
    /// Container for general system settings.
    /// </summary>
    [NotMapped]
    public class GeneralSettings : Settings
    {
        public override string Id
        {
            get { return "GeneralSettings"; }
        }

        public Iteration[] Iterations { get; set; }

        public int CurrentIteration { get { return Iterations.IsEmpty() ? 0 : Iterations.Max(i => i.Id); } }

        public string[] BannedDomains { get; set; }

        /// <summary>
        /// Initializes fields from XML fields.
        /// </summary>
        public override void Initialize()
        {
            Iterations = Get<Iteration[]>(Keys.Iterations);
            BannedDomains = Get<string[]>(Keys.BannedDomains);
            
        }

        /// <summary>
        /// Save fields to XML fields.
        /// </summary>
        public override void Apply()
        {
            Set(Keys.Iterations, Iterations);
            Set(Keys.BannedDomains, BannedDomains);
        }

        public static class Keys
        {
            public const string Iterations = "Terms";
            public const string BannedDomains = "Ban";
        }

        /// <summary>
        /// Represents information about voting iterations.
        /// </summary>
        public class Iteration
        {
            /// <summary>
            /// Number of iteration.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Short user-friendly name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Voting start date.
            /// </summary>
            public DateTime Start { get; set; }

            /// <summary>
            /// Voting end date.
            /// </summary>
            public DateTime End { get; set; }

            /// <summary>
            /// The number of voted users.
            /// </summary>
            [XmlElement("users")]
            public short UniqueUsersCount { get; set; }
        }
    }
}
