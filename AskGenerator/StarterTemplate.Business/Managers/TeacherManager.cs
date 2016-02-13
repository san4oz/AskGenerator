using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Business.Managers
{
    public class TeacherManager : BaseManager<Teacher, ITeacherProvider>, ITeacherManager
    {
        public TeacherManager(ITeacherProvider provider) : base(provider) { }

        public List<Student> GetRelatedStudents(string teacherId)
        {
            return Provider.GetRelatedStudents(teacherId);
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            return Provider.Create(teacher, ids);
        }

        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            return Provider.Update(teacher, ids);
        }
    }
}
