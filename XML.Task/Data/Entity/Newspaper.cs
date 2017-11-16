using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace XML.Task.Entity
{
    public class Newspaper:BaseEntity
    {
        public string PublishCity { get; set; }
        public string PublishingHouse { get; set; }
        public int PublishYear { get; set; }
        public int PageCount { get; set; }
        public int SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}
