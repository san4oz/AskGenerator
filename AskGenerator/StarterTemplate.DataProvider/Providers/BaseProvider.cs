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
    public class BaseProvider
    {
        protected virtual void Execute(Action<AppContext> expression)
        {
            using (var context = new AppContext())
            {
                expression(context);
            }
        }

        protected virtual T Execute<T>(Func<AppContext, T> expression)
        {
            using (var context = new AppContext())
            {
                return expression(context);
            }
        }

        public virtual bool Delete<T>(T entity) where T:class
        {
            return Execute(context =>
            {
                context.Set<T>().Attach(entity);
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.Set<T>().Remove(entity);
                context.SaveChanges();
                return true;
            });
        }

        public virtual List<T> All<T>() where T : class
        {
            return Execute(context =>
            {
                return context.Set<T>().ToList<T>();
            });
        }
    }
}
