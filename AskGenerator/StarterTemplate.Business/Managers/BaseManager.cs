using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AskGenerator.Business.Managers
{
    public abstract class BaseManager
    {
        MemoryCache cache;

        protected abstract string Name { get; }

        public BaseManager()
        {
            cache = MemoryCache.Default;
        }

        #region GetKey
        protected string GetKey<T>(T arg)
        {
            return ToString(arg);
        }

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
        protected TValue FromCache<TValue>(string key, Func<TValue> function, DateTime? expiration = null)
        {
            key = Name + '_' + key;
            var obj = cache.Get(key);
            if (obj != null)
            {
                return (TValue)obj;
            }

            var itemToSave = function();
            ToCache<TValue>(key, () => itemToSave, expiration);

            return itemToSave;
        }

        protected TValue FromCache<TValue>(string key, DateTime? expiration = null)
        {
            key = Name + '_' + key;
            var obj = cache.Get(key);
            if (obj != null)
            {
                var item = (TValue)obj;
                if (item != null)
                    return item;
            }

            return default(TValue);
        }
        #endregion

        #region ToCache
        protected CacheItem ToCache<TValue>(string key, Func<TValue> function, DateTime? expiration = null)
        {
            var itemToSave = function();

            return ToCache(key, itemToSave, expiration);
        }

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

        protected void RemoveFromCache(string key)
        {
            key = Name + '_' + key;
            var removed = RemoveFromCacheCore(key);
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
