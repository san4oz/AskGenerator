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
    public class TeacherQuestionManager : ITeacherQuestionManager
    {
        public ITeacherQuestionProvider Provider {get; private set;}

        public TeacherQuestionManager(ITeacherQuestionProvider provider)
        {
            Provider = provider;
        }


        public bool Create(TeacherQuestion entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TeacherQuestion entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TeacherQuestion entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string teacherId, string questionId)
        {
            throw new NotImplementedException();
        }

        public List<TeacherQuestion> Get(string teacherId, string questionId)
        {
            throw new NotImplementedException();
        }

        public List<TeacherQuestion> List(string teacherId)
        {
            throw new NotImplementedException();
        }

        public List<TeacherQuestion> All()
        {
            throw new NotImplementedException();
        }


        public Task<bool> Save(TeacherQuestion entity)
        {
            throw new NotImplementedException();
        }
    }
}
