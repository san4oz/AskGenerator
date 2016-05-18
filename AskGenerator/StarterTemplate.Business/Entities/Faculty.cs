using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    /// <summary>
    /// Represents faculty of university.
    /// </summary>
    public class Faculty : Entity
    {
        /// <summary>
        /// The name of the faculty.
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// The short name of the faculty.
        /// </summary>
        [StringLength(8)]
        public string ShortName { get; set; }
    }
}
