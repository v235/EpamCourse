using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Entity;
using System.Threading.Tasks;
using CachingLib;
using NorthwindLibrary;

namespace CachingLibRepository
{
    public class CustomersManager:BaseManager
    {
        public CustomersManager(ICachingProvider cache):base(cache)
        {
        }

        //For example
        public void GetCustomers()
        {
            Console.WriteLine("Get Customers count");
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(GetAllData<Customer>().Count());
            }
        }
    }
}
