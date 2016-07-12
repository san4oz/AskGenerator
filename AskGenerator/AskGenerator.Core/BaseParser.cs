using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace AskGenerator.Core
{
    public class BaseParser
    {
        public ParseInfo Info { get; protected set; }

        public virtual void ParseText(string path)
        { }

        protected void ReadLines(string path, Action<IList<string>> action)
        {
            if (File.Exists(path))
            {
                using (var reader = new TextFieldParser(path))
                    ReadLines(reader, action);
            }
        }

        protected void ReadLines(Stream stream, Action<IList<string>> action)
        {
            using (var reader = new TextFieldParser(stream))
                ReadLines(reader, action);
        }

        protected virtual void ReadLines(TextFieldParser parser, Action<IList<string>> action)
        {
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            parser.SetDelimiters(new string[] { ";" });

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                action(row);
            }
        }

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

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
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
