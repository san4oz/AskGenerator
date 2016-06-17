using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
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

        protected readonly ISettingManager Settings;

        public TeamManager(ITeamProvider provider, IHistoryManager historyManager, ISettingManager settingManager)
            : base(provider)
        {
            HistoryManager = historyManager;
            Settings = settingManager;
        }

        public void LoadHistory(Team entity)
        {
            HistoryManager.Get(entity.Id).Apply(entity);
        }


        public void MoveToHistory()
        {
            var all = this.All();
            var iteration = Settings.General(true).CurrentIteration + 1;
            var hist = HistoryManager.GetByPrefix(Team.Prefix);
            var newHistories = new List<History>();

            foreach(var team in all.AsParallel())
            {
                var teamHistory = hist.GetOrDefault(team.Id);
                if (teamHistory == null)
                {
                    teamHistory = new History((IVersionedStatistics<object>)team);
                    newHistories.Add(teamHistory);
                }

                teamHistory.Versions[iteration] = CreateStat(team);
            }
            HistoryManager.Update(hist.Values);
            newHistories.AsParallel().ForAll(h => HistoryManager.Create(h));
        }

        protected virtual Team.Statistics CreateStat(Team team)
        {
            var st =  new Team.Statistics()
            {
                AdditionalMark = team.AdditionalMark,
                AvgDifficult = team.AvgDifficult,
                ClearRating = team.ClearRating,
                Rating = team.Rating
            };
            if (team.Marks != null && team.Marks.Count > 0)
                st.Marks = new SerializableDictionary<string, AnswerCountDictionary>(team.Marks);

            return st;
        }
    }
}
