using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IGroupManager : IBaseEntityManager<Group>
    {
        IGroupProvider Provider { get; }

        List<Group> GetByIds(IEnumerable<string> ids);
    }
}
