using System;
using System.Reflection;
using ConsoleApp1.DAL;
using ConsoleApp1.Logger;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            var container=new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());
            container.AddType(typeof(Parser1));
            container.AddType(typeof(Loger));
            container.AddType(typeof(Repository),typeof(IRepository));
            var r1 = (Parser1)container.CreateInstance(typeof(Parser1));
            r1.parseData();
            Console.WriteLine();
            var r2=(Parser2)container.CreateInstance(typeof(Parser2));
            r2.GetFirstWord();
            Console.ReadKey();
        }
    }
}
