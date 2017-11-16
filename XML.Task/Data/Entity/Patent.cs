using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace XML.Task.Entity
{
    public class Patent:BaseEntity
    {
        public string Creator { get; set; }
        public string Country { get; set; }
        public int SerialNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageCount { get; set; }
    }
}
