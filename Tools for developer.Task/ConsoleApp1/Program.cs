using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNugetLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"['Small','Medium','Large']";
            JsonParser js = new JsonParser();
            var r = js.ParseJson(json);
            var r1 = js.GetFirstElement(json);
            var r2 = js.FindElementByKey(json,"Medium");
            Console.WriteLine("{0},{1}", r1, r2);
            Console.ReadKey();
        }

    }
}
