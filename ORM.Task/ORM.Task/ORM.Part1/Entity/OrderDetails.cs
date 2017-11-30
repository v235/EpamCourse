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
        [Column(Name = "OrderID", IsPrimaryKey = true)]
        public int OrderID { get; set; }

        [Column(Name = "ProductID", IsPrimaryKey = true)]
        public int ProductID { get; set; }

        [Column]
        public decimal UnitPrice { get; set; }

        [Column]
        public  short Quantity { get; set; }

        [Column]
        public bool Discount { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
        public virtual Orders Orders { get; set; }

        [Association(ThisKey = "ProductID", OtherKey = "ProductID")]
        public virtual Products Products { get; set; }
    }
}
