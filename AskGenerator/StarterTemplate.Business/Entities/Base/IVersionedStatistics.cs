using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities.Base
{
    public interface IVersionedStatistics<TStat>
    {
        string Id { get; }

        /// <summary>
        /// Gets prefix for history table.
        /// </summary>
        string HistoryPrefix { get; }

        Dictionary<int, TStat> Versions { get; set; }

        bool LoadStatistics(int iterationID);
    }
}