using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using AskGenerator.Core;

namespace AskGenerator.Business.Managers
{
    /// <summary>
    /// The base class for managers. Contains operations with cache.
    /// </summary>
    public abstract class BaseManager
    {
        protected const string IgnoreCacheScopeName = "ignorecache";
        protected const string UpdateCacheScopeName = "updatecache";

        /// <summary>
        /// Sets flag indicating to ignore cache.
        /// </summary>
        /// <returns>Disposable object.</returns>
        public static IDisposable IgnoreCache()
        {
            return new ThreadScope(IgnoreCacheScopeName);
        }

        /// <summary>
        /// Sets flag indicating to update cache.
        /// </summary>
        /// <returns>Disposable object.</returns>
        public static IDisposable UpdateCache()
        {
            return new ThreadScope(UpdateCacheScopeName);
        }

        MemoryCache cache;

        /// <summary>
        /// Name of the manager. Used as prefix of cache keys.
        /// </summary>
        protected abstract string Name { get; }

        /// <summary>
        /// Creates instance of manager with default state of cache.
        /// </summary>
        public BaseManager()
        {
            cache = MemoryCache.Default;
        }

        #region GetKey
        /// <summary>
        /// Creates cache-key for argument by calling <see cref="ToString"/> method.
        /// </summary>
        /// <typeparam name="T">Type of argument.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <returns>Created key.</returns>
        protected string GetKey<T>(T arg)
        {
            return ToString(arg);
        }

        /// <summary>
        /// Creates cache-key for arguments separated by '.' by calling <see cref="ToString"/> method.
        /// </summary>
        /// <typeparam name="T1">Type of 1-st argument.</typeparam>
        /// <typeparam name="T2">Type of 2-nd argument.</typeparam>
        /// <param name="arg1">Argument 1.</param>
        /// <param name="arg2">Argument 2.</param>
        /// <returns>Created key.</returns>
        protected string GetKey<T1, T2>(T1 arg1, T2 arg2)
        {
            var sb = new StringBuilder(ToString(arg1));
            sb.Append('.');
            sb.Append(ToString(arg2));
            return sb.ToString();
        }

        protected string GetKey<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            var sb = new StringBuilder(ToString(arg1));
            sb.Append('.');
            sb.Append(ToString(arg2));
            sb.Append('.');
            sb.Append(ToString(arg3));
            return sb.ToString();
        }

        /// <summary>
        /// Creates cache-key for an array of arguments separated by '.' by calling <see cref="ToString"/> method.
        /// </summary>
        /// <typeparam name="T">Type of first argument.</typeparam>
        /// <param name="arg1">Argument.</param>
        /// <param name="args">An array of arguments.</param>
        /// <returns>Created key.</returns>
        protected string GetKey<T1>(T1 arg1, params object[] args)
        {
            var sb = new StringBuilder(ToString(arg1));
            for (var i = 0; i < args.Length; i++)
            {
                sb.Append('.');
                sb.Append(ToString(args[i]));
            }
            return sb.ToString();
        }

        #region List
        private const string List = "list_";

        protected string GetListKey()
        {
            return List;
        }

        protected string GetListKey<T>(T arg)
        {
            return List + GetKey(arg);
        }

        protected string GetListKey<T1, T2>(T1 arg1, T2 arg2)
        {
            return List + GetKey(arg1, arg2);
        }

        protected string GetListKey<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            return List + GetKey(arg1, arg2, arg3);
        }

        protected string GetListKey<T1>(T1 arg1, params object[] args)
        {
            return List + GetKey(arg1, args);
        }
        #endregion

        #endregion

        #region FromCache
        /// <summary>
        /// Gets value from cache by specified cache-key.
        /// </summary>
        /// <typeparam name="TValue">Type of expected value.</typeparam>
        /// <param name="key">Cache-key to get value by.</param>
        /// <param name="function">Function to get new value if it was not found in cache.</param>
        /// <param name="expiration">Time before new cache item will be expired.</param>
        /// <returns>Retrived value.</returns>
        protected TValue FromCache<TValue>(string key, Func<TValue> function, DateTime? expiration = null)
        {
            if (ThreadScope.IsInScope(IgnoreCacheScopeName))
                return function();

            if (!ThreadScope.IsInScope(UpdateCacheScopeName))
            {
                var obj = cache.Get(Name + '_' + key);
                if (obj != null)
                {
                    return (TValue)obj;
                }
            }
            var itemToSave = function();
            ToCache<TValue>(key, itemToSave, expiration);

            return itemToSave;
        }


        /// <summary>
        /// Gets value from cache by specified cache-key.
        /// </summary>
        /// <typeparam name="TValue">Type of expected value.</typeparam>
        /// <param name="key">Cache-key to get value by.</param>
        /// <param name="expiration">Time before new cache item will be expired.</param>
        /// <returns>Retrived value.</returns>
        protected TValue FromCache<TValue>(string key)
        {
            key = Name + '_' + key;
            var obj = cache.Get(key);
            if (obj != null)
                return (TValue)obj;

            return default(TValue);
        }
        #endregion

        #region ToCache
        /// <summary>
        /// Adds new item to cache.
        /// </summary>
        /// <typeparam name="TValue">Type of item.</typeparam>
        /// <param name="key">Cache-key to save item by.</param>
        /// <param name="function">Function to get value from.</param>
        /// <param name="expiration">Time before new cache item will be expired.</param>
        /// <returns>Created cache item.</returns>
        protected CacheItem ToCache<TValue>(string key, Func<TValue> function, DateTime? expiration = null)
        {
            var itemToSave = function();

            return ToCache(key, itemToSave, expiration);
        }

        /// <summary>
        /// Adds new item to cache.
        /// </summary>
        /// <typeparam name="TValue">Type of item.</typeparam>
        /// <param name="key">Cache-key to save item by.</param>
        /// <param name="itemToSave">An item to save.</param>
        /// <param name="expiration">Time before new cache item will be expired.</param>
        /// <returns>Created cache item.</returns>
        protected CacheItem ToCache<TValue>(string key, TValue itemToSave, DateTime? expiration = null)
        {
            if (itemToSave == null)
                return null;

            key = Name + '_' + key;
            var cacheItem = RemoveFromCacheCore(key);

            cacheItem = new CacheItem(key, itemToSave);
            cache.Set(cacheItem, policy(expiration));

            return cacheItem;
        }
        #endregion

        /// <summary>
        /// Removes item from cache by its key.
        /// </summary>
        /// <param name="key">Cache-key.</param>
        protected void RemoveFromCache(string key)
        {
            key = Name + '_' + key;
            RemoveFromCacheCore(key);
        }

        /// <summary>
        /// Removes items from cache by their prefix.
        /// </summary>
        /// <param name="key">Prefix cache-keys started with.</param>
        protected void RemoveByPrefix(string prefix)
        {
            var key = Name + '_' + prefix;
            cache.Where(c => c.Key.StartsWith(key, StringComparison.InvariantCultureIgnoreCase))
                .Each(c => RemoveFromCacheCore(c.Key));
        }

        #region private
        private CacheItem RemoveFromCacheCore(string key)
        {
            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem != null)
            {
                cache.Remove(cacheItem.Key);
            }
            return cacheItem;
        }

        private CacheItemPolicy policy(DateTime? expiration = null)
        {
            var policy = new CacheItemPolicy();

            policy.AbsoluteExpiration = expiration.HasValue ? expiration.Value : DateTimeOffset.Now.AddHours(1);

            return policy;
        }

        private string ToString<T>(T arg)
        {
            if (arg == null)
                return string.Empty;
            return arg.ToString();
        }
        #endregion
    }
}
