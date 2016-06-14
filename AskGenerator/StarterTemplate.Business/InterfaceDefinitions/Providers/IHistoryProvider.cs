using AskGenerator.Business.Entities.Base;
using System.Collections.Generic;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface IHistoryProvider : IBaseEntityProvider<History>
    {
        Dictionary<string, History> GetByPrefix(string prefix);
    }
}
