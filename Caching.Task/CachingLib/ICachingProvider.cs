using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingProviders.CachingLib
{
    public interface ICachingProvider
    {
        IEnumerable<T> GetCachedItem<T>(string key);
        void AddItemToCache<T>(string key, T item);
    }
}
