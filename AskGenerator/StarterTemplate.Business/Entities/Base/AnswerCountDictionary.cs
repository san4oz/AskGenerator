using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities.Base
{
    /// <summary>
    /// Keys - answers, values - counts.
    /// </summary>
    [Serializable]
    [XmlRoot("ACDict")]
    public class AnswerCountDictionary : SerializableDictionary<short, int>
    {
        public AnswerCountDictionary()
            : base()
        {
            Avg = new Mark();
        }

        public AnswerCountDictionary(int capacity)
            : base(capacity)
        {
            Avg = new Mark();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="info">A
        /// <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object
        /// containing the information required to serialize the
        /// <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </param>
        /// <param name="context">A
        /// <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure
        /// containing the source and destination of the serialized stream
        /// associated with the
        /// <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </param>
        protected AnswerCountDictionary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public Mark Avg { get; set; }

        public double D()
        {
            double result = 0;
            int totalCount = 0;
            foreach (var key in Keys)
            {
                var count = this[key];
                result = result + Math.Pow(key, 2) * count;
                totalCount += count;
            }
            result /= (double)totalCount;
            return result - Math.Pow(Avg.Answer, 2);
        }
    }
}
