using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AskGenerator.DataProvider.Providers
{
    public class VoteProvider : BaseEntityProvider<Vote>, IVoteProvider
    {
        public List<Vote> List(string userId)
        {
            return GetSet(set => set.Include(x => x.QuestionId).AsQueryable().Where(v => v.AccountId.Equals(userId)).ToList());
        }


        public Vote Get(string userId, string questionId)
        {
            return GetSet(set => set.AsQueryable().FirstOrDefault(v => v.AccountId.Equals(userId) && v.QuestionId.Id.Equals(questionId)));
        }

        public Vote Get(string userId, string teacherId, string questionId)
        {
            return GetSet(set => set.AsQueryable().FirstOrDefault(v => v.AccountId.Equals(userId)
                && v.TeacherId.Equals(teacherId)
                && v.QuestionId.Id.Equals(questionId)));
        }

        public bool Create(Vote vote, string questionId)
        {
            return Execute(context => {
                var question = context.Questions.AsQueryable().FirstOrDefault(q => q.Id == questionId);
                if (question == null)
                    return false;
                vote.QuestionId = question;
                context.Votes.Add(vote);
                context.SaveChanges();
                return true;
            });
        }

        public bool Update(Vote vote, string questionId)
        {
            return Execute(context =>
            {
                if (vote.QuestionId == null)
                {
                    var question = context.Questions.AsQueryable().FirstOrDefault(q => q.Id == questionId);
                    if (question == null)
                        return false;
                    vote.QuestionId = question;
                }
                return base.Update(context, vote);
            });
        }
    }
}
