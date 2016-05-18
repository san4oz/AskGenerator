using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider.Providers
{
    public class FacultyProvider : BaseEntityProvider<Faculty>, IFacultyProvider
    {
        public override List<Faculty> All()
        {
            return GetSet(set => set.AsQueryable().OrderBy(t => t.Name).ToList());
        }
    }
}
