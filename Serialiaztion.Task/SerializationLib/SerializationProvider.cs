using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace SerializationLib
{
    public class SerializationProvider
    {
        private XmlSerializer xmlSerializer;

        public SerializationProvider()
        {
            xmlSerializer = new XmlSerializer(typeof(Library));
        }
        public void DoSerialization(Library objToSerialize)
        {
            
        }

        public Library DoDeSerialization()
        {
            using (StreamReader reader = new StreamReader("books.xml"))
            {   
                return (Library)xmlSerializer.Deserialize(reader);               
            }
            
        }
    }
}
