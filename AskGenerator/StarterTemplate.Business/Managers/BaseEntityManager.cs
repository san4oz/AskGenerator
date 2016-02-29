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
    public abstract class BaseEntityManager<T, TProvider> : BaseManager, IBaseEntityManager<T>
        where T : Entity
        where TProvider : IBaseEntityProvider<T>
    {
        protected TProvider Provider;

        protected override string Name
        {
            get { return "Entity"; }
        }

        public BaseEntityManager(TProvider provider)
        {
            Provider = provider;
        }

        public virtual bool Create(T entity)
        {
            if (entity.Id == null)
                entity.Id = Guid.NewGuid().ToString();
            var r = Provider.Create(entity);
            OnCreated(entity);
            return r;
        }

        /// <summary>
        /// Executes after new entity was created.
        /// </summary>
        /// <param name="entity">New entity.</param>
        protected virtual void OnCreated(T entity)
        {
        }

        public virtual bool Update(T entity)
        {
            return Provider.Update(entity);
        }

        public virtual bool Delete(string id)
        {
            var t = Provider.Delete(id);
            if (t != null)
            {
                OnDeleted(t);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Executes after new entity was created.
        /// </summary>
        /// <param name="entity">New entity.</param>
        protected virtual void OnDeleted(T entity)
        {
        }

        public virtual T Get(string id)
        {
            return Provider.Get(id);
        }

        public virtual Task<T> GetAsync(string id)
        {
            return Task.Factory.StartNew(() => Provider.Get(id));
        }

        public virtual List<T> All()
        {
            return Provider.All();
        }

        public virtual Task<List<T>> AllAsync()
        {
            return Task.Factory.StartNew<List<T>>(this.All);
        }


        public T Extract(string id)
        {
            if (id.IsEmpty())
                return null;
            var t = Provider.Extract(id);
            if (t != null)
                OnDeleted(t);
            return t;
        }
        public Task<T> ExtractAsync(string id)
        {
            return Task.Factory.StartNew(() => Extract(id));

        }
    }
}
