using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AskGenerator.DataProvider.Providers
{
    public class TeacherQuestionProvider : BaseProvider, ITeacherQuestionProvider
    {

        public bool Create(TeacherQuestion entity)
        {
            return Execute(context =>
            {
                context.TeacherQuestion.Add(entity);
                context.SaveChanges();
                return true;
            });
        }

        public bool Update(TeacherQuestion entity)
        {
            return Execute(context =>
            {
                var original = context.TeacherQuestion.First(x => x.QuestionId == entity.QuestionId && x.TeacherId == entity.TeacherId);
                if (original != null)
                {
                    if (original.Equals(entity))
                        return false;

                    context.Entry(original).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            });
        }

        public bool Delete(string teacherId, string questionId)
        {
            return Execute(context =>
            {
                var entity = context.TeacherQuestion.Single(x => x.TeacherId == teacherId && x.QuestionId == questionId);
                context.TeacherQuestion.Remove(entity);
                context.SaveChanges();
                return true;
            });
        }

        public TeacherQuestion Get(string teacherId, string questionId)
        {
            return Execute(context =>
            {
                return context.TeacherQuestion.SingleOrDefault(x => x.TeacherId == teacherId && x.QuestionId == questionId);
            });
        }

        public List<TeacherQuestion> List(string teacherId)
        {
            return Execute(context =>
            {
                return context.TeacherQuestion.AsQueryable().Where(x => x.TeacherId == teacherId).ToList();
            });
        }

        public List<TeacherQuestion> All()
        {
            return base.All<TeacherQuestion>();
        }


        public bool Delete(TeacherQuestion entity)
        {
            return base.Delete(entity);
        }

        #region Stubs
        public override bool Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> All<T>()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
