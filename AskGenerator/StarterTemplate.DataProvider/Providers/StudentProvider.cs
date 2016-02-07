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
    public class StudentProvider : BaseProvider<Student>, IStudentProvider
    {
        public override bool Create(Student item)
        {
            return Execute(context =>
            {
                context.Set<Student>().Add(item);
                context.Entry(item.Group).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            });
        }

        public override Student Get(string id)
        {
            return Execute(context =>
            {
                return context.Students.Include(x => x.Group).SingleOrDefault(x => x.Id == id);
            });
        }

        public override List<Student> All()
        {
            return Execute(context =>
            {
                return context.Students.Include(x => x.Group).Include(x => x.Group.Students).Include(x => x.Group.Teachers).ToList();
            });
        }
    }
}
