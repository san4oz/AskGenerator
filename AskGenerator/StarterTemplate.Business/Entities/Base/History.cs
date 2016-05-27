using System;
using System.Collections.Generic;
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
        string HistoryPrefix { get; set; }

        /// <summary>
        /// The unique ID of versioned entity.
        /// </summary>
        [Index]
        public string Id { get; set; }

        public History()
        { }

        public History(IVersionedStatistics<object> stat)
        {
            Id = stat.Id;
            Fields["Statistics"] = stat.Versions;
        }

        public void Apply<TStat>(IVersionedStatistics<TStat> stat)
        {
            stat.Versions = Fields.GetOrDefault<Dictionary<int, TStat>>("Statistics");
        }
    }
}
