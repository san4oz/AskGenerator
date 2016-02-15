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
    public class VoteManager : BaseManager<Vote, IVoteProvider>, IVoteManager
    {
        public VoteManager(IVoteProvider provider)
            : base(provider)
        {

        }

        public Task<List<Vote>> ListAsync(string userId)
        {
            return new TaskFactory().StartNew(() => Provider.List(userId));
        }

        public Task<bool> Save(Vote vote, string questionId)
        {
            return new TaskFactory().StartNew(() => {
                var prev = Provider.Get(vote.AccountId, vote.TeacherId, questionId);
                if (prev == null)
                {
                    vote.Id = Guid.NewGuid().ToString();
                    return Provider.Create(vote, questionId);
                }
                vote.Id = prev.Id;
                return Provider.Update(vote, questionId);
            });
        }
    }
}
