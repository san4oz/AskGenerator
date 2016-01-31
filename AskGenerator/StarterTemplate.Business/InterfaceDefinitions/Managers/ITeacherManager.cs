using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ITeacherManager : IBaseManager<Teacher>
    {
        List<Student> GetRelatedStudents(string teacherId);
    }
}
