using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Part1.Repositories;

namespace ORM.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            CategoryRepository cr=new CategoryRepository("Northwind");
            var res = cr.GetAllCategory();
            foreach (var c in res)
            {
                Console.WriteLine(c.CategoryName);
            }
            Console.ReadKey();
        }
    }
}
