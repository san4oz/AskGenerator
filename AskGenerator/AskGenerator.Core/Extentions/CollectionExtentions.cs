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
    }
}
