using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingProviders.RedisLib
{
    public interface IRedisProvider
    {
        IEnumerable<T> GetFromRedis<T>(string key);
        void AddToRedis<T>(string key, IEnumerable<T> value);
        void DeleteFromRedis(string key);
    }
}
