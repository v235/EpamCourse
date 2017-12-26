using System;
using SerializationLib;

namespace BasicSerialization.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationProvider sp = new SerializationProvider();
            try
            {   //Before serialization
                var result = sp.DoDeSerialization("books.xml");

                Console.WriteLine("Catalog date: {0}", result.Date.ToString("yyyy-mm-dd"));
                foreach (var el in result.Books)
                {
                    Console.WriteLine(el.Author);
                }

                sp.DoSerialization(result, "newbooks.xml");

                //After serialization
                var result1 = sp.DoDeSerialization("newbooks.xml");

                Console.WriteLine("Catalog date: {0}", result1.Date.ToString("yyyy-mm-dd"));
                foreach (var el in result1.Books)
                {
                    Console.WriteLine(el.Author);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
