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
        const string Col = @"[^\s]+(?:\s[^\s]+)?";
        /// <summary>
        /// Used to parse 'text  columns'.
        /// </summary>
        Regex ParseColumns = new Regex(@"(" + Col + @")(?:\s\s(" + Col + @"| ?))*", RegexOptions.Compiled);

        public ParseInfo Info { get; protected set; }

        public virtual void ParseText(string path)
        {
        }

        public virtual void ParseText(string path, Action<IList<string>> action)
        {
            var lines = ReadLines(path);
            HandleLine(action, lines);

        }

        protected virtual void ParseText(Stream stream, Action<IList<string>> action)
        {
            var lines = ReadLines(stream);
            HandleLine(action, lines);
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

        protected virtual IList<string> ReadLines(Stream stream)
        {
            var result = new List<string>();
            using (var sr = new StreamReader(stream))
                while (!sr.EndOfStream)
                    result.Add(sr.ReadLine());

            return result;
        }

        #region private
        private void HandleLine(Action<IList<string>> action, IList<string> lines)
        {
            foreach (var l in lines)
            {
                var match = ParseColumns.Match(l);
                if (match.Success)
                {
                    var captures = match.Groups[2].Captures;
                    var list = new List<string>(captures.Count);
                    list.Add(match.Groups[1].Value);
                    for (var i = 0; i < captures.Count; i++)
                        list.Add(captures[i].Value);
                    try
                    {
                        action(list);
                    }
                    catch
                    {
                        try
                        {
                            action(list);
                        }
                        catch
                        {
                            if (this.Info != null)
                                this.Info.Failed.Add(list.Join("  "));
                        }
                    }
                }
            }
        }
        #endregion

        #region Nested
        public class ParseInfo : IDisposable
        {
            protected DateTime Begin { get; set; }

            protected DateTime End { get; set; }

            public string Start { get { return Begin.ToString(); } }

            public double Seconds { get { return new TimeSpan(End.Ticks - Begin.Ticks).TotalSeconds; } }

            public int ScippedCount { get { return Scipped.Count; } }

            public int FailedCount { get { return Failed.Count; } }

            public int New { get; set; }

            public List<string> Scipped { get; private set; }

            public List<string> Failed { get; private set; }

            public ParseInfo()
            {
                Scipped = new List<string>();
                Failed = new List<string>();
                Begin = DateTime.Now;
            }

            public void Dispose()
            {
                End = DateTime.Now;
            }
        }
        #endregion
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

        byte index;
        public byte Index
        {
            get { return index; }
            set
            {
                IsInitialized = true;
                index = value;
            }
        }

        /// <summary>
        /// Sets not existing <see cref="Index"/>.
        /// </summary>
        public void NotExists()
        {
            Index = byte.MaxValue;
        }

        /// <summary>
        /// Determines whether column can be found in line by <see cref="Index"/>.
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
            {
                var s = line[column.Index];
                return s.IsEmpty() || s == "*" ? null : s;
            }

            return null;
        }
    }
}
