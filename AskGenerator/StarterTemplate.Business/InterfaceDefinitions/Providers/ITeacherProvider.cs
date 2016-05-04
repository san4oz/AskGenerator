using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface ITeacherProvider : IBaseEntityProvider<Teacher>
    {
        /// <summary>
        /// Loads data from one table.
        /// </summary>
        /// <returns></returns>
        List<Teacher> List();

        bool Create(Teacher teacher, ICollection<string> ids);

        bool Update(Teacher teacher, ICollection<string> ids);
    }
}
