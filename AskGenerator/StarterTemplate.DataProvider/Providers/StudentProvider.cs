using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider.Providers
{
    public class StudentProvider : BaseEntityProvider<Student>, IStudentProvider
    {
        public override bool Create(Student student)
        {
            return Execute(context =>
            {
                student.Group.Teachers = null;
                context.Students.Add(student);
                context.Entry(student.Group).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            });
        }

        public override Student Get(string id)
        {
            return GetSet(set =>
            {
                return set.Include(x => x.Group).Include(x => x.Group.Students).Include(y => y.Group.Teachers).SingleOrDefault(x => x.Id == id);
            });
        }

        public override List<Student> All()
        {
            return GetSet(set =>
            {
                return set.Include(x => x.Group).Include(x => x.Group.Students).Include(x => x.Group.Teachers).ToList();
            });
        }

        public bool MergeOrCreate(Student student)
        {
            return Execute(context =>
            {
                bool created;
                var existing = context.Students.Where(s => s.LastName == student.LastName).Include(x => x.Group)
                    .FirstOrDefault(s => s.Group.Id == student.Group.Id);
                if (existing == null)
                {
                    if (student.Id.IsEmpty())
                        student.Id = Guid.NewGuid().ToString();
                    student.Group.Students = null;
                    context.Students.Add(student);
                    context.Entry(student.Group).State = EntityState.Modified;
                    context.SaveChanges();
                    created = true;
                }
                else
                {
                    existing.Merge(student);
                    if (base.Update(context, existing))
                        context.SaveChanges();
                    created = false;
                }

                return created;
            });
        }


        public List<Student> GroupList(string groupId)
        {
            return GetSet(set => set.Where(s => s.Group.Id == groupId).ToList());
        }

        /// <summary>
        /// Gets list of students by specified faculty ID.
        /// </summary>
        /// <param name="groupId">Faculty identifier.</param>
        /// <returns>List of students.</returns>
        public List<Student> FacultyList(string facultyId)
        {
            return GetSet(set => set.Where(s => s.Group.FacultyId == facultyId).ToList());
        }
    }
}
