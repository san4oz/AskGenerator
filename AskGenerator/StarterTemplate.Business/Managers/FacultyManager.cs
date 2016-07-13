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
    public class FacultyManager : BaseEntityManager<Faculty, IFacultyProvider>, IFacultyManager
    {
        public FacultyManager(IFacultyProvider provider)
            : base(provider)
        {
        }

        protected override string Name { get { return "Faculty"; } }
    }
}
