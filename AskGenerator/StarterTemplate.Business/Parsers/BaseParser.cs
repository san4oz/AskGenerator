using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AskGenerator.Business.Parsers
{
    public class BaseParser
    {
        /// <summary>
        /// Used to parse 'text  columns'.
        /// </summary>
        Regex ParseColumns = new Regex(@"(?:(\w)\s\s)*", RegexOptions.Compiled);

        public virtual void ParseText(string path)
        {
        }

        public virtual void ParseText(string path, Action<IList<string>> action)
        {
            var lines = ReadLines(path);
            foreach (var l in lines)
            {
                var match = ParseColumns.Match(l);
                if (match.Success)
                {
                    var captures = match.Groups[1].Captures;
                    var list = new List<string>(captures.Count);
                    for (var i = 0; i < captures.Count; i++)
                        list.Add(captures[i].Value);
                    action(list);
                }
            }
        }

        protected virtual IList<string> ReadLines(string path)
        {
            var result = new List<string>();
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                    while (!sr.EndOfStream)
                        result.Add(sr.ReadLine());
            }
            return result;
        }
    }
}
