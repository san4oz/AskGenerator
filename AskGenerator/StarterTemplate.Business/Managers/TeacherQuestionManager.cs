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
    public class TeacherQuestionManager : ITeacherQuestionManager
    {
        public ITeacherQuestionProvider Provider { get; private set; }

        public TeacherQuestionManager(ITeacherQuestionProvider provider)
        {
            Provider = provider;
        }

        public bool Create(TeacherQuestion entity)
        {
            return Provider.Create(entity);
        }

        public bool Update(TeacherQuestion entity)
        {
            return Provider.Update(entity);
        }

        #region Delete
        public bool Delete(TeacherQuestion entity)
        {
            return Provider.Delete(entity);
        }

        public void Delete(string teacherId)
        {
            Provider.Delete(teacherId);
        }

        public bool Delete(string teacherId, string questionId)
        {
            return Provider.Delete(teacherId, questionId);
        }
        #endregion

        public TeacherQuestion Get(string teacherId, string questionId)
        {
            return Provider.Get(teacherId, questionId);
        }

        public List<TeacherQuestion> List(string teacherId)
        {
            return Provider.List(teacherId);
        }

        public List<TeacherQuestion> All()
        {
            return Provider.All();
        }


        public Task<bool> AddAnswer(TeacherQuestion entity)
        {
            return Task.Factory.StartNew(() =>
            {
                var row = Get(entity.TeacherId, entity.QuestionId);
                if (row == null)
                {
                    Create(entity);
                    return true;
                }

                row.Add(entity);
                Update(row);
                return false;
            });
        }


        public Task<bool> AddExistingAnswer(TeacherQuestion entity, short prevAnswer)
        {
            return Task.Factory.StartNew(() =>
            {
                var row = Get(entity.TeacherId, entity.QuestionId);
                if (row == null)
                {
                    Create(entity);
                    return true;
                }

                row.Merge(entity, new TeacherQuestion() { Answer = prevAnswer });
                Update(row);
                return false;
            });
        }
    }
}
