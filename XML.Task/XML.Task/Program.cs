using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XML.Task.DAL;
using XML.Task.Entity;
using XMLLibrary;

namespace XML.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlManager manager = new XmlManager(new BooksRepository(), new NewspapersRepository(), new PatentsRepository());
            FileStream fs1 = new FileStream("library.xml", FileMode.OpenOrCreate);
            manager.WriteXmlFile(fs1, "http://epam.com/library");
            fs1.Close();

            //read XML
            FileStream fs2 = new FileStream("library.xml", FileMode.Open);
            var res = manager.ReadXMLFile(fs2).Where(e => e.GetType() == typeof(Patent));
            foreach (var r in res)
            {
                var r1 = (Patent)r;
                Console.WriteLine(r1.RequestDate);
            }
            fs2.Close();
            Console.ReadKey();
        }
    }
}
