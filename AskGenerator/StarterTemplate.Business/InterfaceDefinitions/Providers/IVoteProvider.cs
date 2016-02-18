using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface IVoteProvider : IBaseEntityProvider<Vote>
    {
        List<Vote> List(string userId);

        bool Create(Vote vote, string questionId);

        bool Update(Vote vote, string questionId);

        Vote Get(string userId, string questionId);

        Vote Get(string userId, string teacherId, string questionId);
    }
}
