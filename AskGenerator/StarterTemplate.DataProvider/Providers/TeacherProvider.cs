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
    public class TeacherProvider : BaseEntityProvider<Teacher>, ITeacherProvider
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
                    if (teacher.Id.IsEmpty())
                        teacher.Id = Guid.NewGuid().ToString();
                    if (ids != null && ids.Count > 0)
                    {
                        var groups = context.Groups.Where(x => ids.Contains(x.Id))
                            .Include(x => x.Students).Include(x => x.Teachers)
                            .ToList();
                        teacher.Groups = groups;
                    }
                    else
                    {
                        teacher.Groups = new List<Group>();
                    }
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
                    .Include(x => x.Groups.Select(y => y.Students))
                    .OrderBy(x => x.FirstName)
                    .ToList();
            });
        }


        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            return Execute(context =>
            {
               if (ids!=null)
               {
                var groups = context.Groups.Where(x => ids.Contains(x.Id))
                    .Include(x => x.Students).Include(x => x.Teachers)
                    .ToList();
                    teacher.Groups = groups;
               }
                return Update(context, teacher);
            });
        }

        protected override bool Update(AppContext context, Teacher teacher)
        {
            var original = context.Teachers.First(x => x.Id == teacher.Id);
            if (original != null)
            {
                if (teacher.HasEmptyFields)
                    original.CopyFieldsTo(teacher);

                if (original.Equals(teacher))
                    return false;
                
                var deletedGroups = original.Groups.Where(g => teacher.Groups.FirstOrDefault(g2 => g2.Id == g.Id) == null);
                foreach (var g in deletedGroups.ToList())
                {
                    g.Teachers.Remove(original);
                    context.Entry(g).State = EntityState.Modified;
                }
                foreach (var g in teacher.Groups.ToList())
                {
                    if (!g.Teachers.Contains(teacher))
                    {
                        g.Teachers.Add(original);
                        context.Entry(g).State = EntityState.Modified;
                    }
                }
                context.Entry(original).CurrentValues.SetValues(teacher);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all entities without includings.
        /// </summary>
        /// <returns>The list of teachers.</returns>
        public List<Teacher> List()
        {
            return new AppContext().Teachers.AsQueryable().ToList();
        }
    }
}
