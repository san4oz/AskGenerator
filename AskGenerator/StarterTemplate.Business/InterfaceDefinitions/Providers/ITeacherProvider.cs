using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface ITeacherProvider : IBaseProvider<Teacher>
    {
        List<Student> GetRelatedStudents(string teacherId);

        bool Create(Teacher teacher, ICollection<string> ids);
    }
}
