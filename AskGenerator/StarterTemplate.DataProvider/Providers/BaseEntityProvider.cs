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
    public class BaseEntityProvider<T> : BaseProvider, IBaseEntityProvider<T>
        where T : Entity
    {

        protected virtual TEntity GetSet<TEntity>(Func<DbSet<T>, TEntity> expression)
        {
            using (var context = new AppContext())
            {
                return expression(context.Set<T>());
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
                return Update(context, entity);
            });
        }

        public virtual void Update(IEnumerable<T> sequance)
        {
            Execute(context =>
            {
                Update(context, sequance);
            });
        }

        protected virtual bool Update(AppContext context, T entity)
        {
            var original = context.Set<T>().First(x => x.Id == entity.Id);
            if (original != null)
            {
                if (original.Equals(entity))
                    return false;
                context.Entry(original).CurrentValues.SetValues(entity);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        protected virtual void Update(AppContext context, IEnumerable<T> entities)
        {
            bool changed = false;
            foreach (var entity in entities)
            {
                var original = context.Set<T>().First(x => x.Id == entity.Id);
                if (original != null)
                {
                    if (original.Equals(entity))
                        continue;
                    context.Entry(original).CurrentValues.SetValues(entity);
                    changed = true;
                }
            }
            if(changed)
                context.SaveChanges();
        }

        public new bool Delete(T entity)
        {
            return base.Delete(entity);
        }

        public virtual T Delete(string id)
        {
            return Execute(context =>
            {
                var entity = context.Set<T>().SingleOrDefault(x => x.Id == id);
                if (entity == null)
                    return entity;
                context.Set<T>().Remove(entity);
                context.SaveChanges();
                return entity;
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
            return base.All<T>();
        }


        public T Extract(string id)
        {
            return Execute(context =>
            {
                var entity = context.Set<T>().Single(x => x.Id == id);
                if(entity != null){
                    context.Set<T>().Remove(entity);
                    context.SaveChanges();
                }
                return entity;
            });
        }
    }
}
