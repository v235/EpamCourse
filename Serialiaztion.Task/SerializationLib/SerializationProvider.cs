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
        public void DoSerialization(Library objToSerialize, string filePath)
        {
            try
            {
                using (StreamWriter fs = new StreamWriter(new FileStream("newbooks.xml", FileMode.OpenOrCreate), Encoding.UTF8))
                {
                    xmlSerializer.Serialize(fs, objToSerialize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Library DoDeSerialization(string filePath)
        {
            try
            {
                using (StreamReader fs = new StreamReader(new FileStream("newbooks.xml", FileMode.Open), Encoding.UTF8))
                {
                    return (Library)xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
