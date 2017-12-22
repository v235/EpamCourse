using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CachingLibInvalidationCache;
using NorthwindLibrary;

namespace InvalidationCache.Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Runtime.Cache
            //var cache = new RuntimeCacheMonitor();
            //var res = cache.Select();
            //foreach (var e in res)
            //{
            //    Console.WriteLine(e.LastName);
            //}

            //var res1 = cache.Select();
            //foreach (var e1 in res1)
            //{
            //    Console.WriteLine(e1.LastName);
            //}
            //var res2 = cache.Select();
            //foreach (var e2 in res2)
            //{
            //    Console.WriteLine(e2.LastName);
            //}

            //var res3 = cache.Select();
            //foreach (var e3 in res3)
            //{
            //    Console.WriteLine(e3.LastName);
            //}

            //Redis.Cache
            var cache = new RedisCacheMonitor();
            var res = cache.Select();
            foreach (var e in res)
            {
                Console.WriteLine(e.LastName);
            }

            var res1 = cache.Select();
            foreach (var e1 in res1)
            {
                Console.WriteLine(e1.LastName);
            }
            var res2 = cache.Select();
            foreach (var e2 in res2)
            {
                Console.WriteLine(e2.LastName);
            }

            var res3 = cache.Select();
            foreach (var e3 in res3)
            {
                Console.WriteLine(e3.LastName);
            }

            Console.ReadKey();
        }
    }
}
