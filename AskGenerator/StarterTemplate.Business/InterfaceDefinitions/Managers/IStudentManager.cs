using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IStudentManager : IBaseManager<Student>
    {
        /// <summary>
        /// Creates new or merges with existing.
        /// Search existing student by <see cref="Student.FirstName"/> and <see cref="Student.Group"/>.
        /// </summary>
        /// <param name="student">The student to create or merge.</param>
        /// <returns>Value indicating whether new entity was created.</returns>
        bool MergeOrCreate(Student student);
    }
}
