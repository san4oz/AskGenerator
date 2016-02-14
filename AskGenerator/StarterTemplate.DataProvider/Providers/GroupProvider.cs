using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AskGenerator.DataProvider.Providers
{
    public class GroupProvider : BaseProvider<Group>, IGroupProvider
    {
        public override List<Group> All()
        {
            return Execute(context =>
            {
                return context.Groups.Include(x => x.Students).Include(x => x.Teachers).ToList();
            });
        }

        public override Group Get(string id)
        {
            return GetSet(set =>
            {
                return set.Include(x => x.Teachers).SingleOrDefault(g => g.Id == id);
            });
        }

        public List<Group> GetById(IEnumerable<string> ids)
        {
            return Execute(context =>
            {
                return context.Groups.Include(x => x.Students).Include(x => x.Teachers).Where(x => ids.Contains(x.Id)).ToList();
            });
        }
    }
}
