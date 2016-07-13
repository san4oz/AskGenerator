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

        public BaseEntityManager(TProvider provider)
        {
            Provider = provider;
        }

        #region Create
        /// <summary>
        /// Saves new instance of entity.
        /// </summary>
        /// <param name="entity">Created entity to save.</param>
        /// <returns><c>true</c> if saving was completed successfuly.</returns>
        public virtual bool Create(T entity)
        {
            if (entity.Id == null)
                entity.Id = Guid.NewGuid().ToString();
            entity.Apply();
            var r = Provider.Create(entity);
            if(r)
                OnCreated(entity);
            return r;
        }

        /// <summary>
        /// Executes after new entity was created.
        /// </summary>
        /// <param name="entity">New entity.</param>
        protected virtual void OnCreated(T entity)
        {
            RemoveFromCache(GetListKey());
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates modified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="applyFields">Indicates whether XML fields should be initialized from <paramref name="entity"/> or used saved one.</param>
        /// <returns><c>true</c> if entity's record was updated.</returns>
        public virtual bool Update(T entity, bool applyFields = true)
        {
            if(applyFields)
                entity.Apply();

            var result = Provider.Update(entity);
            if (result)
                OnUpdated(entity);
            return result;
        }

        /// <summary>
        /// Updates entities.
        /// </summary>
        /// <param name="sequence">The sequence of entities to update.</param>
        /// <param name="applyFields">Indicates whether XML fields should be initialized from each entity or used saved ones.</param>
        public virtual void Update(IEnumerable<T> sequence, bool applyFields = true)
        {
            var list = (sequence as IList<T>) ?? sequence.ToList();
            if(applyFields)
                sequence.Each(t => t.Apply());
            Provider.Update(list);
            OnUpdating(list);
        }

        /// <summary>
        /// Executes after entity was modified.
        /// </summary>
        /// <param name="entity">Modified entity.</param>
        protected virtual void OnUpdated(T entity)
        {
            RemoveFromCache(GetListKey());
            RemoveFromCache(entity.Id);
        }

        /// <summary>
        /// Executes after entities were modified. Clears list and id cache.
        /// </summary>
        /// <param name="entities">Modified entities.</param>
        protected virtual void OnUpdating(IList<T> entities)
        {
            RemoveFromCache(GetListKey());
            entities.Each(e => RemoveFromCache(e.Id));
        }
        #endregion

        #region Delete
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
        /// Executes after entity was deleted.
        /// </summary>
        /// <param name="entity">Deleted entity.</param>
        protected virtual void OnDeleted(T entity)
        {
            RemoveFromCache(GetListKey());
            RemoveFromCache(entity.Id);
        }
        #endregion

        public virtual T Get(string id)
        {
            if (id.IsEmpty())
                throw new ArgumentNullException("id", "Id can not be empty.");

            return FromCache(GetKey(id), () =>
            {
                var t = Provider.Get(id);
                if (t != null) t.Initialize();
                return t;
            });
        }

        public virtual Task<T> GetAsync(string id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public virtual List<T> All()
        {
            return FromCache(GetListKey(), () =>
            {
                var list = Provider.All();
                list.Each(t => t.Initialize());
                return list;
            });
        }

        public virtual Task<List<T>> AllAsync()
        {
            return Task.Factory.StartNew<List<T>>(this.All);
        }


        public T Extract(string id)
        {
            if (id.IsEmpty())
                return null;
            var t = Provider.Delete(id);
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
