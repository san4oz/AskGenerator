using AskGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class CacheManager : BaseManager
    {
        protected override string Name
        {
            get { return "cache"; }
        }
        /// <summary>
        /// Gets value from cache by specified cache-key.
        /// </summary>
        /// <typeparam name="TValue">Type of expected value.</typeparam>
        /// <param name="key">Cache-key to get value by.</param>
        /// <param name="function">Function to get new value if it was not found in cache.</param>
        /// <returns>Retrived value.</returns>
        public TValue Get<TValue>(string key, Func<TValue> function)
        {
            return FromCache(key, function, DateTime.Now.AddHours(2));
        }

        /// <summary>
        /// Removes item from cache by its key.
        /// </summary>
        /// <param name="key">Cache-key.</param>
        public void Remove(string key)
        {
            RemoveFromCache(key);
        }

        /// <summary>
        /// Sets flag indicating to ignore cache.
        /// </summary>
        public IDisposable Ignore
        {
            get { return new ThreadScope(IgnoreCacheScopeName); }
        }

        /// <summary>
        /// Sets flag indicating to update cache.
        /// </summary>
        public IDisposable Update
        {
            get { return new ThreadScope(UpdateCacheScopeName); }
        }
    }
}
