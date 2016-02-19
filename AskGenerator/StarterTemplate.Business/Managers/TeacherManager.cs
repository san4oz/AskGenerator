using AskGenerator.Business.Entities;
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
    public class TeacherManager : BaseManager<Teacher, ITeacherProvider>, ITeacherManager
    {
        protected ITeacherQuestionManager TQ { get; private set; }

        public TeacherManager(ITeacherProvider provider, ITeacherQuestionManager tqManager)
            : base(provider)
        {
            TQ = tqManager;
        }

        public List<Student> GetRelatedStudents(string teacherId)
        {
            return Provider.GetRelatedStudents(teacherId);
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            return Provider.Create(teacher, ids);
        }

        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            return Provider.Update(teacher, ids);
        }

        public List<Teacher> List()
        {
            return Provider.List();
        }

        public Task<List<Teacher>> All(bool loadMarks)
        {
            return new TaskFactory().StartNew(() => {
                var teachers = this.List();
                if (!loadMarks)
                    return teachers;
                var answers = TQ.All().ToLookup(tq => tq.TeacherId);
                foreach (var t in teachers)
                {
                    var list = answers[t.Id];
                    t.Marks = list.Select(x => new Mark() { Answer = x.Answer, QuestionId = x.QuestionId }).ToList();
                    float avg = t.Marks.Aggregate(0f, (a, m) => a+m.Answer);
                    if (avg != 0)
                        avg /= (float)t.Marks.Count;
                    else
                        avg = -0.001f;
                    t.Marks.Insert(0, new Mark() { Answer = avg, QuestionId = Question.AvarageId });

                }
                return teachers.OrderByDescending(t => t.Marks[0].Answer).ToList();
            });
        }
    }
}
