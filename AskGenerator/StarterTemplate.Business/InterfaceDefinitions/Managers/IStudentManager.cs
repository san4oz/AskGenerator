using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface IStudentManager : IBaseEntityManager<Student>
    {
        /// <summary>
        /// Creates new or merges with existing.
        /// Search existing student by <see cref="Student.FirstName"/> and <see cref="Student.Group"/>.
        /// </summary>
        /// <param name="student">The student to create or merge.</param>
        /// <returns>Value indicating whether new entity was created.</returns>
        bool MergeOrCreate(Student student);

        /// <summary>
        /// Gets list of students by specified group ID.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        /// <returns>List of students.</returns>
        IList<Student> GroupList(string groupId);

        /// <summary>
        /// Gets list of students by specified faculty ID.
        /// </summary>
        /// <param name="groupId">Faculty identifier.</param>
        /// <returns>List of students.</returns>
        IList<Student> FacultyList(string facultyId);
    }
}
