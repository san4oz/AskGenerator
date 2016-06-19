using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class HistoryManager : BaseEntityManager<History, IHistoryProvider>, IHistoryManager
    {
        public HistoryManager(IHistoryProvider provider)
            : base(provider)
        { }

        public async void DeleteIteraion(int id)
        {
            var list = await AllAsync();
            foreach (var note in list)
                note.GetVersions<IDictionary>().Remove(id);
            await Task.Factory.StartNew(() => Provider.Update(list));
        }

        public Dictionary<string, History> GetByPrefix(string prefix)
        {
            if (prefix.IsEmpty())
                throw new ArgumentNullException("prefix", "Prefix shouldn't be an empty string.");

            return Provider.GetByPrefix(prefix);
        }
    }
}
