using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities.Base
{
    public class RatableGroupsStatistic
    {
        public virtual Mark Rating { get; set; }

        /// <summary>
        /// Answer-count pairs per question ID.
        /// </summary>
        public virtual SerializableDictionary<string, AnswerCountDictionary> Marks { get; set; }
    }
}
