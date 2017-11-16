using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace XML.Task.Entity
{
    [Serializable]
    [System.Xml.Serialization.XmlRoot("catalog")]
    public class Book:BaseEntity
    {

        public string Authors { get; set; }
        public string PublishCity { get; set; }
        public string PublishingHouse { get; set; }
        public int PublishYear { get; set; }
        public int PageCount { get; set; }

        public string ISBN { get; set; }
    }
}
