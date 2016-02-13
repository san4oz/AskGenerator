using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AskGenerator.DataProvider.Providers
{
    public class TeacherProvider : BaseProvider<Teacher>, ITeacherProvider
    {
        public override Teacher Get(string id)
        {
            return Execute(context =>
            {
                return context.Teachers.Include("Groups").SingleOrDefault(x => x.Id == id);
            });
        }

        public override bool Create(Teacher entity)
        {
            return Execute(context =>
            {
                context.Teachers.Add(entity);                               
                context.SaveChanges();
                return true;
            });
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            return Execute(context =>
                {
                    var groups = context.Groups.Include(x => x.Students).Include(x => x.Teachers).Where(x => ids.Contains(x.Id)).ToList();
                    teacher.Groups = groups;
                    context.Teachers.Add(teacher);
                    context.SaveChanges();
                    return true;
                });
        }

        public override List<Teacher> All()
        {
            return Execute(context =>
            {
                return context.Teachers.Include(x => x.Groups)
                    .Include(x => x.Groups.Select(y => y.Teachers))
                    .Include(x => x.Groups.Select(y => y.Students)).ToList();
            });
        }

        public List<Student> GetRelatedStudents(string id)
        {
            return Execute(context =>
            {
                var teacher = context.Teachers.Include(x => x.Groups).Single(x => x.Id == id);

                var ids = teacher.Groups.Select(x => x.Id).ToList();

                return context.Students.Include(g => g.Group).Include(x => x.Group.Students).Include(x => x.Group.Teachers).Where(s => ids.Contains(s.Group.Id)).ToList();
            });
        }


        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            return Execute(context =>
            {
                var entry = context.Entry<Teacher>(teacher);
                if (entry.State == EntityState.Detached)
                    context.Teachers.Attach(teacher);

                var groups = context.Groups.Include(x => x.Students).Include(x => x.Teachers).Where(x => ids.Contains(x.Id)).ToList();
                teacher.Groups = groups;

                entry.State = EntityState.Modified;
                context.SaveChanges();
                return true;
            });
        }
    }
}
