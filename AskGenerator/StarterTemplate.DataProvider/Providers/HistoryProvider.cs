using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
using AskGenerator.Business.Entities.Settings;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider.Providers
{
    public class HistoryProvider : BaseEntityProvider<History>, IHistoryProvider
    {
        public Dictionary<string, History> GetByPrefix(string prefix)
        {
            return GetSet(s => s.Where(h => h.HistoryPrefix == prefix).ToDictionary(h => h.Id));
        }
    }
}
