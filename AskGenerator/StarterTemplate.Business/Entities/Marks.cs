using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Mark
    {
        public virtual string QuestionId { get; set; }

        /// <summary>
        /// Gets or sets avarage answer.
        /// </summary>
        public virtual float Answer { get; set; }
    }

    public class TeacherQuestion : Mark
    {
        public TeacherQuestion()
        {
            Count = 1;
        }

        public static TeacherQuestion operator +(TeacherQuestion entity1, TeacherQuestion entity2)
        {
            var result = new TeacherQuestion()
            {
                TeacherId = entity1.TeacherId,
                QuestionId = entity1.QuestionId,
                Count = entity1.Count,
                Answer = entity1.Answer,
                Teacher = entity1.Teacher,
                Question = entity2.Question
            };
            result.Count += entity2.Count;
            result.Answer += entity2.Answer;
            result.Answer /= (float)result.Count;
            return result;
        }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Question")]
        public override string QuestionId { get; set; }

        /// <summary>
        /// Gets or sets count of answers.
        /// </summary>
        public int Count { get; set; }

        public Teacher Teacher { get; set; }
        public Question Question { get; set; }
    }
}
