using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class GroupManager : BaseManager<Group, IGroupProvider>, IGroupManager
    {
        public GroupManager(IGroupProvider provider) : base(provider) { }

        public List<Group> GetById(IEnumerable<String> ids)
        {
            return ((IGroupProvider)Provider).GetById(ids);
        }
    }
}
