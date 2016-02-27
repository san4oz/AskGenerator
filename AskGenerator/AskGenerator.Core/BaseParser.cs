using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AskGenerator.Core
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

    public class Column
    {
        #region Ctors
        public Column(string key)
            : this(key, byte.MaxValue)
        {
        }

        public Column(string key, byte value)
        {
            Key = key;
            Index = value;
            IsInitialized = Exists;
        }
        #endregion

        public string Key { get; protected set; }

        public byte Index { get; set; }

        /// <summary>
        /// Sets not existing <see cref="Index"/>.
        /// </summary>
        public void NotExists()
        {
            Index = byte.MaxValue;
        }

        /// <summary>
        /// Determines whether column can be found if line by <see cref="Index"/>.
        /// </summary>
        public bool Exists { get { return Index != byte.MaxValue; } }

        /// <summary>
        /// Determines whether column <see cref="Index"/> has default value and could not be used.
        /// </summary>
        public bool IsInitialized { get; set; }
    }

    public static class LineExtentions
    {
        public static string Col(this IList<string> line, Column column)
        {
            if (!column.IsInitialized)
                throw new InvalidOperationException("Not initialized column");
            if (column.Exists)
                return line[column.Index];

            return null;
        }
    }
}
