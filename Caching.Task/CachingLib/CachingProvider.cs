using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingProviders.CachingLib
{
    public class CachingProvider: ICachingProvider
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;

        public IEnumerable<T> GetCachedItem<T>(string key)
        {
            try
            {
                return (IEnumerable<T>)Cache[key];
            }
            catch
            {
                return null;
            }
        }
        public void AddItemToCache<T>(string key, T item)
        {
            Cache.Add(key, item, DateTimeOffset.Now.AddMinutes(1));
        }
    }
}
