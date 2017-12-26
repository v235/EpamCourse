using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SerializationLib
{
    [Serializable, XmlRoot(Namespace = "http://library.by/catalog", ElementName = "catalog")]
    public class Library
    {
        public Library()
        {
            Books = new List<Book>();
        }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }

        [XmlElement("book")]
        public List<Book> Books { get; set; }
    }
}
