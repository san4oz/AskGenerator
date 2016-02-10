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
    }
}
