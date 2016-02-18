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
    public abstract class BaseManager<T, TProvider> : IBaseManager<T>
        where T : Entity
        where TProvider : IBaseEntityProvider<T>
    {
        protected TProvider Provider;

        public BaseManager(TProvider provider)
        {
            Provider = provider;
        }

        public virtual bool Create(T entity)
        {
            if (entity.Id == null)
                entity.Id = Guid.NewGuid().ToString();
            return Provider.Create(entity);
        }

        public virtual bool Update(T entity)
        {
            return Provider.Update(entity);
        }

        public virtual bool Delete(string id)
        {
            return Provider.Delete(id);
        }

        public virtual T Get(string id)
        {
            return Provider.Get(id);
        }

        public virtual Task<T> GetAsync(string id)
        {
            return new TaskFactory().StartNew(() => Provider.Get(id));
        }

        public virtual List<T> All()
        {
            return Provider.All();
        }
    }
}
