using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XMLLibrary;

namespace XML.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlManager manager = new XmlManager();
            var xml = manager.CreateXml("http://epam.com/library/");
            foreach (var el in manager.GetDataFromXML(xml, "book"))
            {
                var res=el.Value.Select(e => e);
                Console.WriteLine(el.Name);
                Console.WriteLine(el);
            }
            
            //Console.WriteLine(xml);
            Console.ReadKey();
        }
    }
}
