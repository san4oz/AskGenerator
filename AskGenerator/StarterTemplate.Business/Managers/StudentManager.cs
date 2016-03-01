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
    public class StudentManager : BaseEntityManager<Student, IStudentProvider>, IStudentManager
    {
        protected override string Name
        {
            get { return "Student"; }
        }

        public StudentManager(IStudentProvider provider) : base(provider) { }

        public bool MergeOrCreate(Student student)
        {
            return Provider.MergeOrCreate(student);
        }

        public IList<Student> GroupList(string groupId)
        {
            if (groupId.IsEmpty())
                return new Student[0];
            var key = GetListKey(groupId);

            return FromCache(key, () => Provider.GroupList(groupId));
        }

        protected override void OnCreated(Student student)
        {
            var key = GetListKey(student.Group.Id);
            base.RemoveFromCache(key);
        }
    }
}
