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

        Dictionary<int, TStat> Versions { get; set; }

        bool InitStatistics(int iterationID);
    }
}