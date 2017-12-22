using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CachingProviders.CachingLib;
using CachingProviders.RedisLib;

namespace Caching.Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numForFibbonachi = new int[] { 7, 20, 25, 7, 40, 32, 20};

            RuntimeCaching(numForFibbonachi);
            //RedisCaching(numForFibbonachi);
            Console.ReadKey();

        }
#region Caching
        //RuntimeCaching
        public static void RuntimeCaching(int [] numForFibbonachi)
        {
            CachingProvider cp = new CachingProvider();
            for (int i=0;i<numForFibbonachi.Length;i++)
            {
                Console.WriteLine("For item {0} Fibonacci sequence is:", numForFibbonachi[i]);
                var sequenceFromCache = cp.GetCachedItem<int>(numForFibbonachi[i].ToString());
                if (sequenceFromCache != null)
                {
                    Console.WriteLine("value from Cache");
                    WriteResult(sequenceFromCache);
                }
                else
                {
                    var sequence = GetFibonacciSequence(numForFibbonachi[i]);
                    cp.AddItemToCache(numForFibbonachi[i].ToString(), sequence);
                    WriteResult(sequence);
                }
            }
        }

        //Redis
        public static void RedisCaching(int[] numForFibbonachi)
        {
            var redisCache = new RedisProvider();
            for (int i = 0; i < numForFibbonachi.Length; i++)
            {
                Console.WriteLine("For item {0} Fibonacci sequence is:", numForFibbonachi[i]);
                var sequenceFromCache = redisCache.GetFromRedis<int>(numForFibbonachi[i].ToString());
                if (sequenceFromCache!=null)
                {
                    Console.WriteLine("value from Cache");
                    WriteResult(sequenceFromCache);
                }
                else
                {
                    var sequence = GetFibonacciSequence(numForFibbonachi[i]);
                    redisCache.AddToRedis(numForFibbonachi[i].ToString(), sequence);
                    WriteResult(sequence);
                }
            }
        }
        private static void WriteResult(IEnumerable<int> result)
        {
            foreach(var i in result)
            {
                Console.Write("{0},",i);
            }
            Console.WriteLine();
        }
        private static IEnumerable<int> GetFibonacciSequence(int count)
        {
            if (count < 0)
                throw new ArgumentException("GetFibonacciSequence should throw ArgumentException if count is negative");
            int priv = 0;
            int cur = 1;
            for (int i = 1; i < count; i++)
            {
                if (i < 2)
                    yield return 1;
                int next = cur + priv;
                yield return next;
                priv = cur;
                cur = next;
            }
        }
#endregion
    }
}
