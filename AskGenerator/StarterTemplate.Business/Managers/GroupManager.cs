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
    public class GroupManager : BaseEntityManager<Group, IGroupProvider>, IGroupManager
    {

        public GroupManager(IGroupProvider provider) : base(provider) { }

        public List<Group> GetByIds(IEnumerable<String> ids)
        {
            var list = ((IGroupProvider)Provider).GetByIds(ids);
            list.Each(g => g.Initialize());
            return list;
        }

        public new IGroupProvider Provider
        {
            get { return base.Provider; }
        }
    }
}
