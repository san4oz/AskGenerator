using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IBaseEntityManager<T>
    {
        bool Create(T entity);

        bool Update(T entity);

        bool Delete(string id);

        T Extract(string id);

        /// <summary>
        /// Deletes and returns entity by specified ID.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Deleted entity or <c>null</c> if entity was not found.</returns>
        Task<T> ExtractAsync(string id);

        T Get(string id);

        Task<T> GetAsync(string id);

        List<T> All();

        Task<List<T>> AllAsync();
    }
}
