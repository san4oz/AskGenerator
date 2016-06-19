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

        public History(string id, string prefix)
        {
            Id = id;
            HistoryPrefix = prefix;
        }

        /// <summary>
        /// Sets history to Versions property.
        /// </summary>
        /// <typeparam name="TStat"></typeparam>
        /// <param name="stat">Object to set history to.</param>
        public void Apply<TStat>(IVersionedStatistics<TStat> stat)
        {
            stat.Versions = Fields.GetOrDefault<SerializableDictionary<int, TStat>>("Versions");
        }

        public IDictionary<int, TStat> GetVersions<TStat>()
        {
            return Fields.GetOrCreate<SerializableDictionary<int, TStat>>("Versions");
        }

        /// <summary>
        /// Sets versions to history.
        /// </summary>
        /// <typeparam name="TStat"></typeparam>
        public void SetVersions<TStat>(IDictionary<int, TStat> versions)
        {
            Fields["Versions"] = new SerializableDictionary<int, TStat>(versions);
        }
    }
}
