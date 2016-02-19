using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface ITeacherQuestionProvider
    {
        bool Create(TeacherQuestion entity);

        bool Update(TeacherQuestion entity);

        bool Delete(TeacherQuestion entity);

        bool Delete(string teacherId, string questionId);

        TeacherQuestion Get(string teacherId, string questionId);

        List<TeacherQuestion> List(string teacherId);

        List<TeacherQuestion> All();
    }
}
