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
        protected override string Name { get { return "Student"; } }

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

        #region Cleaning cache
        protected override void OnCreated(Student entity)
        {
            base.OnCreated(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
        }

        protected override void OnUpdated(Student entity)
        {
            base.OnUpdated(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
        }

        protected override void OnDeleted(Student entity)
        {
            base.OnDeleted(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
        }

        protected override void OnUpdating(IList<Student> entities)
        {
            base.OnUpdating(entities);
            entities.Each(e => RemoveFromCache(GetListKey(e.Group.Id)));
        }
        #endregion
    }
}
