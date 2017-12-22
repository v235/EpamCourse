using System;
using System.Linq;
using CachingLibRepository;
using CachingProviders.CachingLib;
using CachingProviders.RedisLib;
using NorthwindLibrary;

namespace Caching.Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test for baseManager runtime caching
            //var baseManager = new BaseManager(new CachingProvider());
            //for (var i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(baseManager.GetAllData<Customer>().Count());
            //}

            //Test for baseManager runtime redis 
            var baseManager = new BaseManager(new RedisProvider());
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(baseManager.GetAllData<Customer>().Count());
            }

            Console.ReadKey();
        }
    }
}
