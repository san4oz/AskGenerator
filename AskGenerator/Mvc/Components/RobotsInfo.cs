using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.Components
{
    /// <summary>
    /// Class for robots / search engines page settings.
    /// </summary>
    public class RobotsInfo
    {
        public RobotsInfo()
        {
            KeyWords = new List<string>();
            Index = Follow = true;
        }

        /// <summary>
        /// Determines whether robots should index current page.
        /// </summary>
        public bool Index { get; set; }

        /// <summary>
        /// Determines whether robots should follow links on current page.
        /// </summary>
        public bool Follow { get; set; }

        /// <summary>
        /// Key fords for current page.
        /// </summary>
        public List<string> KeyWords { get; protected set; }

        /// <summary>
        /// Description of the page.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Converts <see cref="Index"/> and <see cref="Follow"/> settings to string. (e.g. index,follow or no-index,follow)
        /// </summary>
        /// <returns>String contains instructions for meta-robots tag.</returns>
        public override string ToString()
        {
            return (Index ? "index," : "no-index,") + (Follow ? "follow" : "no-follow");  // index,follow index,no-follow no-index,follow
        }

        private const string baseKeyWords = "ZSTU, ЖДТУ, Evaluator, Рейтинг, Житомир, Університет, Викладачі, Дошка, Оцінювання, Voting, Rating";

        /// <summary>
        /// Formats <see cref="KeyWords"/> and adds base key words separated by coma.
        /// </summary>
        /// <returns>String contains key words.</returns>
        public virtual string GetKeyWords()
        {
            var str = KeyWords.Join(", ");
            if (KeyWords.Count > 0)
                str += ", ";
            return str + baseKeyWords;
        }
    }
}
