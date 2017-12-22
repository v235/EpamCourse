using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Entity;
using System.Threading.Tasks;
using CachingLib;

namespace CachingLibRepository
{
    public class CategoriesManager:BaseManager
    {
        public CategoriesManager(ICachingProvider cache):base(cache)
        {
        }

        //For example
        public void GetCategories()
        {
            Console.WriteLine("Get Categories count");
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(GetAllData<Category>().Count());
            }
        }
    }
}
