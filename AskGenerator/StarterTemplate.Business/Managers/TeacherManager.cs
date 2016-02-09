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
    public class TeacherManager : BaseManager<Teacher, ITeacherProvider>, ITeacherManager
    {
        public TeacherManager(ITeacherProvider provider) : base(provider) { }

        public List<Student> GetRelatedStudents(string teacherId)
        {
            return ((ITeacherProvider)Provider).GetRelatedStudents(teacherId);
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            return ((ITeacherProvider)Provider).Create(teacher, ids);
        }
    }
}
