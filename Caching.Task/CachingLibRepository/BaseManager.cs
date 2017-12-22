using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CachingProviders.CachingLib;
using CachingProviders.RedisLib;

namespace CachingLibRepository
{
    public class BaseManager:IBaseManager
    {
        private ICachingProvider cache;
        private IRedisProvider redisCache;

        public BaseManager(ICachingProvider cache)
        {
            this.cache = cache;
        }
        public BaseManager(IRedisProvider redisCache)
        {
                this.redisCache = redisCache;
        }
        public IEnumerable<T> GetAllData<T>() where T : class
        {
            if (cache != null)
                return GetDataForRuntimeCache<T>();
            return GetDataForRedisCache<T>();
        }
        private IEnumerable<T> GetDataForRuntimeCache<T>() where T : class
        {
            var key = Thread.CurrentPrincipal.Identity.Name;
            var result = cache.GetCachedItem<T>(key);
            if (result == null)
            {
                Console.WriteLine("From DB");
                result = GetDataFromDB<T>();
                cache.AddItemToCache(key, result);
            }
            else
            {
                Console.WriteLine("From cache");
            }

            return result;
        }
        private IEnumerable<T> GetDataForRedisCache<T>() where T : class
        {
            var key = Thread.CurrentPrincipal.Identity.Name;
            var result = redisCache.GetFromRedis<T>(key);
            if (result == null)
            {
                Console.WriteLine("From DB");
                result = GetDataFromDB<T>();
                redisCache.AddToRedis(key, result);
            }
            else
            {
                Console.WriteLine("From cache");
            }

            return result;
        }
        private IEnumerable<T>GetDataFromDB<T>()where T:class
        {
            using (var dbContext = new Northwind())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<T>().ToList();
            }
        }
    }
}
