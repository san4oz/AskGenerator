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
    public class GroupManager : BaseEntityManager<Group, IGroupProvider>, IGroupManager
    {
        protected readonly IHistoryManager HistoryManager;

        public GroupManager(IGroupProvider provider,IHistoryManager historyManager) : base(provider) 
        {
            this.HistoryManager = historyManager;
        }

        public List<Group> GetByIds(IEnumerable<String> ids)
        {
            var list = ((IGroupProvider)Provider).GetByIds(ids);
            list.Each(g => g.Initialize());
            return list;
        }

        public new IGroupProvider Provider
        {
            get { return base.Provider; }
        }

        public Group LoadHistory(Group entity)
        {
            HistoryManager.Get(entity.Id).Apply(entity);
            return entity;
        }

        public void LoadHistory(IList<Group> entities)
        {
            var notes = HistoryManager.GetByPrefix(Group.Prefix);
            foreach (var entity in entities.AsParallel())
            {
                var history = notes.GetOrDefault(entity.Id);
                if (history != null)
                    history.Apply(entity);
            }
        }

        public void MoveToHistory()
        {
            var all = this.All();
            var iteration = Site.Settings.General(true).CurrentIteration + 1;
            var hist = HistoryManager.GetByPrefix(Group.Prefix);
            var newHistories = new List<History>();

            foreach (var group in all.AsParallel())
            {
                var groupHistory = hist.GetOrDefault(group.Id);
                if (groupHistory == null)
                {
                    groupHistory = new History(group.Id, Group.Prefix);
                    newHistories.Add(groupHistory);
                }

                groupHistory.GetVersions<Group.Statistics>()[iteration] = CreateStat(group);
            }
            HistoryManager.Update(hist.Values);
            newHistories.AsParallel().ForAll(h => HistoryManager.Create(h));
        }

        protected virtual Group.Statistics CreateStat(Group group)
        {
            var st = new Group.Statistics()
            {
                AverageVote = group.AverageVote,
                StudentsCount = group.StudentsCount,
                Rating = group.Rating
            };
            if (group.Marks != null && group.Marks.Count > 0)
                st.Marks = new SerializableDictionary<string, AnswerCountDictionary>(group.Marks);

            return st;
        }
    }
}
