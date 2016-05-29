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
    public class TeamManager : BaseEntityManager<Team, ITeamProvider>, ITeamManager
    {
        protected readonly IHistoryManager HistoryManager;

        public TeamManager(ITeamProvider provider, IHistoryManager historyManager)
            : base(provider)
        {
            HistoryManager = historyManager;
        }

        public void LoadHistory(Team entity)
        {
            HistoryManager.Get(entity.Id).Apply(entity);
        }
    }
}
