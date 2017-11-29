using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table(Name = "Orders")]
    public class Orders
    {
        [PrimaryKey, Identity]
        public int OrderID { get; set; }

        [Column]
        public string CustomerID { get; set; }

        [Association(ThisKey = "CustomerID", OtherKey = "CustomerID")]
        public Customers Customers { get; set; }

        [Column]
        public int EmployeeID { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public Employees Employees { get; set; }

        [Column]
        public DateTime OrderDate { get; set; }

        [Column]
        public  DateTime RequiredDate { get; set; }

        [Column]
        public DateTime ShippedDate { get; set; }

        [Column]
        public int ShipVia { get; set; }

        [Association(ThisKey = "ShipVia", OtherKey = "ShipperID")]
        public Shippers Shippers { get; set; }

        [Column]
        public decimal Freight { get; set; }

        [Column]
        public string ShipName { get; set; }

        [Column]
        public string ShipAddress { get; set; }

        [Column]
        public string ShipCity { get; set; }

        [Column]
        public string ShipRegion { get; set; }

        [Column]
        public string ShipPostalCode { get; set; }

        [Column]
        public string ShipCountry { get; set; }

    }
}
