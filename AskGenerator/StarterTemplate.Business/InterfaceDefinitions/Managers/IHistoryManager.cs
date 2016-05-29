using AskGenerator.Business.Entities.Base;
using System.Collections.Generic;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IHistoryManager : IBaseEntityManager<History>
    {
        void DeleteIteraion(int id);

        Dictionary<string, History> GetByPrefix(string prefix);
    }
}
