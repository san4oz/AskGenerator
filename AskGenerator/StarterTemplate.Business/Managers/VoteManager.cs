using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Business.Managers
{
    public class VoteManager : BaseEntityManager<Vote, IVoteProvider>, IVoteManager
    {
        protected ITeacherQuestionManager TQ { get; private set; }

        public VoteManager(IVoteProvider provider, ITeacherQuestionManager tqManager)
            : base(provider)
        {
            TQ = tqManager;
        }

        public Task<List<Vote>> ListAsync(string userId)
        {
            return Task.Factory.StartNew(() => Provider.List(userId));
        }

        public Task<bool> Save(Vote vote, string questionId)
        {
            return Task.Factory.StartNew(() =>
            {
                var prev = Provider.Get(vote.AccountId, vote.TeacherId, questionId);
                bool success;
                if (prev == null)
                {
                    vote.Id = Guid.NewGuid().ToString();
                    success = Provider.Create(vote, questionId);
                }
                else
                {
                    vote.Id = prev.Id;
                    success = Provider.Update(vote, questionId);
                }
                if (success)
                {
                    TQ.Save(new TeacherQuestion()
                    {
                        QuestionId = questionId,
                        TeacherId = vote.TeacherId,
                        Answer = vote.Answer
                    }).Wait();
                }
                return success;
            });
        }
    }
}
