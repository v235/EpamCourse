using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name = "Order Details")]
    public class OrderDetails
    {
        [PrimaryKey]
        public int OrderID { get; set; }

        [PrimaryKey]
        public int ProductID { get; set; }

        [Column]
        public decimal UnitPrice { get; set; }

        [Column]
        public  short Quantity { get; set; }

        [Column]
        public bool Discount { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
