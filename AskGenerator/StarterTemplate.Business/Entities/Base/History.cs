using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities.Base
{
    public class History : Entity
    {
        /// <summary>
        /// Gets prefix for history table.
        /// </summary>
        [Index]
        [MaxLength(16)]
        public string HistoryPrefix { get; set; }

        /// <summary>
        /// The unique ID of versioned entity.
        /// </summary>
        public override string Id { get; set; }

        public History()
        { }

        public IDictionary<int, object> Versions { get { return Fields.GetOrDefault<Dictionary<int, object>>("Versions"); } }

        public History(IVersionedStatistics<object> stat)
        {
            Id = stat.Id;
            HistoryPrefix = stat.HistoryPrefix;
            Fields["Versions"] = stat.Versions;
        }

        /// <summary>
        /// Sets history to Versions property.
        /// </summary>
        /// <typeparam name="TStat"></typeparam>
        /// <param name="stat">Object to set history to.</param>
        public void Apply<TStat>(IVersionedStatistics<TStat> stat)
        {
            stat.Versions = Fields.GetOrDefault<Dictionary<int, TStat>>("Versions");
        }
    }
}
