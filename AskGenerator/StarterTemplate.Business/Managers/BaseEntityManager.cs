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
            entity.Apply();
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

        public virtual bool Update(T entity, bool applyFields = true)
        {
            if(applyFields)
                entity.Apply();
            return Provider.Update(entity);
        }

        public virtual void Update(IEnumerable<T> sequence, bool applyFields = true)
        {
            if(applyFields)
                sequence.Each(t => t.Apply());
            Provider.Update(sequence);
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
            var t = Provider.Get(id);
            if(t != null) t.Initialize();
            return t;
        }

        public virtual Task<T> GetAsync(string id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public virtual List<T> All()
        {
            var list = Provider.All();
            list.Each(t => t.Initialize());
            return list;
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
            {
                t.Initialize();
                OnDeleted(t);
            }
            return t;
        }
        public Task<T> ExtractAsync(string id)
        {
            return Task.Factory.StartNew(() => Extract(id));

        }
    }
}
