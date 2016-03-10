using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class CollectionExtentions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            var t = default(TValue);
            dictionary.TryGetValue(key, out t);
            return t;
        }

        public static string Join(this IList<string> source, string delimiter)
        {
            var sb = new StringBuilder(source.First());
            for (var i = 1; i < source.Count; i++)
                sb.Append(delimiter + source[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Returns with elements from sequence in randomized order.
        /// </summary>
        /// <typeparam name="T">Type of sequence's elements.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <returns>Shuffled sequence.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rnd = new Random();
            return source.OrderBy(x => rnd.Next());
        }
    }
}
