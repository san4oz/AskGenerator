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
    public class BaseProvider<T> : IBaseProvider<T>
        where T : Entity
    {

        protected virtual TEntity GetSet<TEntity>(Func<DbSet<T>, TEntity> expression)
        {
            using (var context = new AppContext())
            {
                return expression(context.Set<T>());
            }
        }

        protected virtual TEntity Execute<TEntity>(Func<AppContext, TEntity> expression)
        {
            using (var context = new AppContext())
            {
                return expression(context);
            }
        }

        protected virtual bool Execute(Func<AppContext, bool> expression)
        {
            using (var context = new AppContext())
            {
                return expression(context);
            }
        }

        public virtual bool Create(T entity)
        {
            return Execute(context =>
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
                return true;
            });
        }

        public virtual bool Update(T entity)
        {
            return Execute(context =>
            {
                //context.Set<T>().Attach(entity);
                //context.Entry<T>(entity).State = EntityState.Modified;
                //context.SaveChanges();
                //return true;
                var original = context.Set<T>().First(x => x.Id == entity.Id);
                if(original != null)
                {
                    context.Entry(original).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            });
        }

        public virtual bool Delete(string id)
        {
            return Execute(context =>
            {
                var entity = context.Set<T>().Single(x => x.Id == id);
                context.Set<T>().Remove(entity);
                context.SaveChanges();
                return true;
            });
        }

        public virtual T Get(string id)
        {
            return Execute(context =>
            {
                return context.Set<T>().SingleOrDefault(x => x.Id == id);
            });
        }

        public virtual List<T> All()
        {
            return Execute(context =>
            {
                return context.Set<T>().ToList<T>();
            });
        }


    }
}
