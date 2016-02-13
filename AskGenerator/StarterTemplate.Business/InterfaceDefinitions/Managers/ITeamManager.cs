using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ITeamManager : IBaseManager<Team>
    {
        /// <summary>
        /// Deletes and returns team by specified ID.
        /// </summary>
        /// <param name="id">ID of the team.</param>
        /// <returns>Deleted team or <c>null</c> if team was not found.</returns>
        Task<Team> Delete(string id);
    }
}
