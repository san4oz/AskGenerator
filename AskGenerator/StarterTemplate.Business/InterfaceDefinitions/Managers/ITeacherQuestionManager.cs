using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ITeacherQuestionManager
    {
        ITeacherQuestionProvider Provider { get; }

        bool Create(TeacherQuestion entity);

        bool Update(TeacherQuestion entity);

        Task<bool> Save(TeacherQuestion entity);

        bool Delete(TeacherQuestion entity);

        bool Delete(string teacherId, string questionId);

        List<TeacherQuestion> Get(string teacherId, string questionId);

        List<TeacherQuestion> List(string teacherId);

        List<TeacherQuestion> All();
    }
}
