using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Base;
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
    public class TeacherManager : BaseEntityManager<Teacher, ITeacherProvider>, ITeacherManager
    {
        protected override string Name { get { return "Teacher"; } }

        protected ITeacherQuestionManager TQ { get; private set; }
        protected readonly IHistoryManager HistoryManager;


        public TeacherManager(ITeacherProvider provider, IHistoryManager historyManager, ITeacherQuestionManager tqManager)
            : base(provider)
        {

            HistoryManager = historyManager;
            TQ = tqManager;
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            var r = Provider.Create(teacher, ids);
            if (r)
                OnCreated(teacher);
            return r;
        }

        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            var result = Provider.Update(teacher, ids);
            if (result)
                OnUpdated(teacher);
            return result;
        }

        protected List<Teacher> List()
        {
            var list = Provider.List();
            foreach (var t in list) t.Initialize();
            return list;
        }

        public List<Teacher> All(bool loadMarks)
        {
            return FromCache(GetListKey("marks", loadMarks), () =>
            {
                var teachers = this.List();
                if (!loadMarks)
                    return teachers;
                var answers = TQ.All().ToLookup(tq => tq.TeacherId);
                foreach (var t in teachers)
                {
                    t.Marks = answers[t.Id].ToList();
                    float avg = 0;
                    int count = 0;
                    t.Marks = t.Marks.Where(m => m.Answer != 0).ToList();
                    foreach (var mark in t.Marks)
                    {
                        avg += mark.Answer;
                        count++;
                    }
                    if (avg != 0)
                        avg /= (float)count;
                    else
                        avg = -0.001f;
                    t.Marks.Insert(0, new TeacherQuestion() { Answer = avg, QuestionId = Question.AvarageId });
                }
                return teachers;
            });
        }

        public Task<List<Teacher>> AllAsync(bool loadMarks)
        {
            return Task.Factory.StartNew(() => All(loadMarks));
        }

        #region IHistoryLoader<Teacher> members
        public void MoveToHistory(int iterationId)
        {
            var all = this.All();
            var hist = HistoryManager.GetByPrefix(Teacher.Prefix);
            var newHistories = new List<History>();

            foreach (var team in all.AsParallel())
            {
                var teamHistory = hist.GetOrDefault(team.Id);
                if (teamHistory == null)
                {
                    teamHistory = new History(team.Id, Teacher.Prefix);
                    newHistories.Add(teamHistory);
                }

                teamHistory.GetVersions<Teacher.Statistics>()[iterationId] = CreateStat(team);
            }
            HistoryManager.Update(hist.Values);
            newHistories.AsParallel().ForAll(h => HistoryManager.Create(h));
        }

        protected virtual Teacher.Statistics CreateStat(Teacher teacher)
        {
            return new Teacher.Statistics()
            {
                Badges =  teacher.Badges,
                VotesCount = teacher.VotesCount
            };
        }

        public Teacher LoadHistory(Teacher entity)
        {
            HistoryManager.Get(entity.Id).Apply(entity);
            return entity;
        }

        public void LoadHistory(IList<Teacher> entities)
        {
            var notes = HistoryManager.GetByPrefix(Teacher.Prefix);
            foreach (var entity in entities.AsParallel())
            {
                var history = notes.GetOrDefault(entity.Id);
                if (history != null)
                    history.Apply(entity);
            }
        }
        #endregion

        #region Clearing cache
        protected override void OnCreated(Teacher entity)
        {
            base.OnCreated(entity);
            RemoveByPrefix(GetListKey("marks"));
        }

        protected override void OnUpdated(Teacher entity)
        {
            base.OnUpdated(entity);
            RemoveByPrefix(GetListKey("marks"));
        }

        protected override void OnDeleted(Teacher entity)
        {
            base.OnDeleted(entity);
            RemoveByPrefix(GetListKey("marks"));
        }

        protected override void OnUpdating(IList<Teacher> entities)
        {
            base.OnUpdating(entities);
            RemoveByPrefix(GetListKey("marks"));
        }
        #endregion
    }
}