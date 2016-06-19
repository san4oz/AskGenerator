using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IHistoryLoader<TEntity>
    {
        /// <summary>
        /// Loads history to specified <see cref="entity"/>.
        /// </summary>
        /// <param name="entity">Entity to load history.</param>
        TEntity LoadHistory(TEntity entity);

        /// <summary>
        /// Loads history to specified <see cref="entities"/>.
        /// </summary>
        /// <param name="entities">List of entities to load history.</param>
        void LoadHistory(IList<TEntity> entities);

        /// <summary>
        /// Moves all entities statistic to the history.
        /// </summary>
        void MoveToHistory();
    }
}
