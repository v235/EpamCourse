using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Redis;
using ServiceStack.DataAnnotations;

namespace CachingProviders.RedisLib
{
    public class RedisProvider: IRedisProvider
    {
        RedisManagerPool redisManager;
        IRedisClient redis;
        public RedisProvider()
        {
            redisManager = new RedisManagerPool("localhost:6379");
            redis = redisManager.GetClient();
        }

        public void AddToRedis<T>(string key, IEnumerable<T> value)
        {
            redis.Add(key, value.ToList());
        }
        public IEnumerable<T> GetFromRedis<T>(string key)
        {
            try
            {
                return redis.Get<IEnumerable<T>>(key);
            }
            catch
            {
                return null;
            }
        }
        public void DeleteFromRedis(string key)
        {
            redis.Remove(key);
        }
    }
}
