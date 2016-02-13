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
    public class TeamManager : BaseManager<Team, ITeamProvider>, ITeamManager
    {
        public TeamManager(ITeamProvider provider)
            : base(provider)
        {

        }

        public Task<Team> Delete(string id)
        {
            return new TaskFactory<Team>().StartNew(() =>
            {
                var team = Get(id);
                if (team != null)
                    Delete(team);
                return team;
            });
            
        }
    }
}
