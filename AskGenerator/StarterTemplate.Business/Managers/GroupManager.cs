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
        protected override string Name { get { return "Group"; } }

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

        #region IHistoryLoader<Group> members
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

        public void MoveToHistory(int iterationId)
        {
            var all = this.All();
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

                groupHistory.GetVersions<Group.Statistics>()[iterationId] = CreateStat(group);
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
        #endregion

        public IList<Group> GetByFaculty(string facultyId)
        {
            if (facultyId.IsEmpty())
                return new Group[0];

            var key = GetListKey(facultyId);
            return FromCache(key, () => Provider.GetByFaculty(facultyId));
        }

        #region Clearing cache
        protected override void OnCreated(Group entity)
        {
            base.OnCreated(entity);
            RemoveFromCache(GetListKey(entity.FacultyId));
        }

        protected override void OnUpdated(Group entity)
        {
            OnCreated(entity);
        }

        protected override void OnUpdating(IList<Group> entities)
        {
            base.OnUpdating(entities);
            entities.GroupBy(e => e.FacultyId)
                .Each(g => RemoveFromCache(GetListKey(g.Key)));
        }

        protected override void OnDeleted(Group entity)
        {
            base.OnDeleted(entity);
            RemoveFromCache(GetListKey(entity.FacultyId));
        }
        #endregion
    }
}
