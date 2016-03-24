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

        /// <summary>
        /// Gets or sets the number of answers.
        /// </summary>
        public int Count { get; set; }
    }

    public class TeacherQuestion : Mark
    {
        public TeacherQuestion()
        {
            Count = 1;
        }

        public void Add(TeacherQuestion entity2)
        {
            this.Answer = this.Answer * this.Count + entity2.Answer * entity2.Count;
            this.Count += entity2.Count;
            this.Answer /= (float)this.Count;
        }

        public void Merge(TeacherQuestion entity2, TeacherQuestion prevEntity)
        {
            this.Answer = this.Answer * this.Count + entity2.Answer * entity2.Count - prevEntity.Answer * prevEntity.Count;
            this.Count += entity2.Count - prevEntity.Count;
            this.Answer /= (float)this.Count;
        }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Question")]
        public override string QuestionId { get; set; }

        public Teacher Teacher { get; set; }
        public Question Question { get; set; }
    }
}
