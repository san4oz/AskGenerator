using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ITeacherManager : IBaseEntityManager<Teacher>, IHistoryLoader<Teacher>
    {
        bool Create(Teacher teacher, ICollection<string> ids);

        bool Update(Teacher teacher, ICollection<string> ids);

        List<Teacher> All(bool loadMarks);

        Task<List<Teacher>> AllAsync(bool loadMarks);
    }
}
