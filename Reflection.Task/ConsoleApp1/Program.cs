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
            var container1=new Container();
            container1.AddAssembly(Assembly.GetExecutingAssembly());
            var r1 = (Parser1)container1.CreateInstance(typeof(Parser1));
            r1.parseData();
            Console.WriteLine();
            var r2 = (Parser2)container1.CreateInstance(typeof(Parser2));
            r2.GetFirstWord();

            var container2 =new Container();
            container2.AddType(typeof(Parser1));
            container2.AddType(typeof(Loger));
            container2.AddType(typeof(Repository), typeof(IRepository));
            var r3 = container2.CreateInstance<Parser1>();
            r1.parseData();
            Console.WriteLine();
            var r4 = container2.CreateInstance<Parser2>();
            r2.GetFirstWord();

            Console.ReadKey();
        }
    }
}
