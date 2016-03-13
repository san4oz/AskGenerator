using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IVoteManager : IBaseEntityManager<Vote>
    {
        Task<List<Vote>> ListAsync(string userId);

        Task<bool> Save(Vote vote, string questionId);

        int UniqueUserCount();
    }
}
