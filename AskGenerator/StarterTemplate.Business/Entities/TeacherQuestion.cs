using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class TeacherQuestion
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Question")]
        public string QuestionId { get; set; }

        /// <summary>
        /// Gets or sets avarage answer.
        /// </summary>
        public int Answer { get; set; }

        /// <summary>
        /// Gets or sets count of answers.
        /// </summary>
        public int Count { get; set; }

        public Teacher Teacher { get; set; }
        public Question Question { get; set; }
    }
}
