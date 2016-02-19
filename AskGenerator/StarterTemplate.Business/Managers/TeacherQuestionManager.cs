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
        public ITeacherQuestionProvider Provider {get; private set;}

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

        public bool Delete(TeacherQuestion entity)
        {
            return Provider.Delete(entity);
        }

        public bool Delete(string teacherId, string questionId)
        {
            return Provider.Delete(teacherId, questionId);
        }

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


        public Task<bool> Save(TeacherQuestion entity)
        {
            return new TaskFactory().StartNew(() =>
            {
                var row = Get(entity.TeacherId, entity.QuestionId);
                if (row == null)
                {
                    Create(entity);
                    return true;
                }

                entity += row;
                Update(entity);
                return false;
            });
        }
    }
}
