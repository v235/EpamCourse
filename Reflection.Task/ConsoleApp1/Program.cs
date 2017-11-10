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
            container.AddType(typeof(Repository));
            //container.AddType(typeof(Repository),typeof(IRepository));
            var r=(Parser1)container.CreateInstance(typeof(Parser1));
            r.parseData();
            //var clasesImportConstructor = asm.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute(typeof(ImportConstructorAttribute)) != null);
            //var propertyPublicImport =
            //    asm.GetTypes().Where(t =>
            //        t.IsClass && t.IsPublic).Select(c => c.GetProperty("CustomerDAL"));
            //    //.Select(p =>
            //    //    p.GetProperties() GetCustomAttribute(typeof(ImportAttribute))!=null);
            //foreach (var p in propertyPublicImport)
            //{
            //    //if (p != null)
            //    //{
            //    //    p.SetValue();
            //    //}
            //}
            ////t.GetCustomAttribute(typeof(ImportAttribute))!=null)
            ////.Select(c => c.GetProperties());         
            ////object[] param = { new CustomerDAL(), new Logger() };
            //foreach (var c in clasesImportConstructor)
            //{
            //    //var createInstance = Activator.CreateInstance(c, param);
            //}
            Console.ReadKey();
        }
    }
}
