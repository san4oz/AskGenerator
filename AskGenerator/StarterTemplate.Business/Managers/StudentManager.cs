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

        /// <summary>
        /// Gets list of students by specified group ID.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        /// <returns>List of students.</returns>
        public IList<Student> GroupList(string groupId)
        {
            if (groupId.IsEmpty())
                return new Student[0];

            var key = GetListKey(groupId);
            return FromCache(key, () => Provider.GroupList(groupId));
        }

        /// <summary>
        /// Gets list of students by specified faculty ID.
        /// </summary>
        /// <param name="groupId">Faculty identifier.</param>
        /// <returns>List of students.</returns>
        public IList<Student> FacultyList(string facultyId)
        {
            if(facultyId.IsEmpty())
                return new Student[0];

            var key = FacultyCache(facultyId);
            return FromCache(key, () => Provider.FacultyList(facultyId));
        }

        protected string FacultyCache(string facultyId)
        {
            return GetListKey("faculty_", facultyId);
        }

        #region Cleaning cache
        protected override void OnCreated(Student entity)
        {
            base.OnCreated(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
            RemoveFromCache(FacultyCache(entity.Group.FacultyId));
        }

        protected override void OnUpdated(Student entity)
        {
            base.OnUpdated(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
            RemoveFromCache(FacultyCache(entity.Group.FacultyId));
        }

        protected override void OnDeleted(Student entity)
        {
            base.OnDeleted(entity);
            RemoveFromCache(GetListKey(entity.Group.Id));
            RemoveFromCache(FacultyCache(entity.Group.FacultyId));
        }

        protected override void OnUpdating(IList<Student> entities)
        {
            base.OnUpdating(entities);
            entities.Each(e =>
            {
                RemoveFromCache(GetListKey(e.Group.Id));
                RemoveFromCache(FacultyCache(e.Group.FacultyId));

            });
        }
        #endregion
    }
}
