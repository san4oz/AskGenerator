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

                return context.Students.Where(s => teacher.Groups.Contains(s.Group)).ToList();
            });
        }
    }
}
