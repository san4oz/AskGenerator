﻿using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ITeacherManager : IBaseEntityManager<Teacher>
    {
        List<Student> GetRelatedStudents(string teacherId);

        bool Create(Teacher teacher, ICollection<string> ids);

        bool Update(Teacher teacher, ICollection<string> ids);

        /// <summary>
        /// Loads list of teachers.
        /// </summary>
        /// <returns>Task with loading.</returns>
        Task<List<Teacher>> ListAsync();

        Task<List<Teacher>> AllAsync(bool loadMarks);
    }
}
