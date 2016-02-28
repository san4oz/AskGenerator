using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Business.Entities
{
    public class Question : Entity
    {
        public string QuestionBody { get; set; }

        public string LowerRateDescription { get; set; }

        public string HigherRateDescription { get; set; }

        public bool IsAboutTeacher { get; set; }

        public const string AvarageId = "AVARAGEID";

        /// <summary>
        /// Gets or sets <see cref="Limit"/> for best badge.
        /// </summary>
        public Limit RightLimit { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Limit"/> for worst badge.
        /// </summary>
        public Limit LeftLimit { get; set; }

        public class Limit
        {
            public float AvgLimit { get; set; }

            /// <summary>
            /// Gets or sets path to the person image.
            /// </summary>
            public string Image { get; set; }

            [MaxLength(50)]
            public string ToolTip { get; set; }

            [MaxLength(250)]
            public string Description { get; set; }
        }
    }


}
