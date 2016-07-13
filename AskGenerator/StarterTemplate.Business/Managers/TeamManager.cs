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
        protected override string Name { get { return "Team"; } }

        protected readonly IHistoryManager HistoryManager;

        public TeamManager(ITeamProvider provider, IHistoryManager historyManager, ISettingManager settingManager)
            : base(provider)
        {
            HistoryManager = historyManager;
        }

        public Team LoadHistory(Team entity)
        {
            HistoryManager.Get(entity.Id).Apply(entity);
            return entity;
        }

        public void LoadHistory(IList<Team> entities)
        {
            var notes = HistoryManager.GetByPrefix(Team.Prefix);
            foreach (var entity in entities)
            {
                var history = notes.GetOrDefault(entity.Id);
                if (history != null)
                    history.Apply(entity);
            }
        }

        public void MoveToHistory(int iterationId)
        {
            var all = this.All();
            var hist = HistoryManager.GetByPrefix(Team.Prefix);
            var newHistories = new List<History>();

            foreach(var team in all.AsParallel())
            {
                var teamHistory = hist.GetOrDefault(team.Id);
                if (teamHistory == null)
                {
                    teamHistory = new History(team.Id, Team.Prefix);
                    newHistories.Add(teamHistory);
                }

                teamHistory.GetVersions<Team.Statistics>()[iterationId] = CreateStat(team);
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
