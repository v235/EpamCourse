using System;
using SerializationLib;

namespace BasicSerialization.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationProvider sp = new SerializationProvider();

            var result=sp.DoDeSerialization();

            Console.WriteLine("Catalog date: {0}", result.Date);
            foreach (var el in result.Books)
            {
                Console.WriteLine(el.Author);
            }

            sp.DoSerialization(result);

            Console.ReadKey();
        }
    }
}
