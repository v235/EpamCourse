using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table(Name = "Customers")]
    public class Customers
    {
        [PrimaryKey, Identity]
        public string CustomerID { get; set; }

        [Column]
        public string CompanyName { get; set; }

        [Column]
        public string ContactName { get; set; }

        [Column]
        public string ContactTitle { get; set; }

        [Column]
        public string Address { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string Region { get; set; }

        [Column]
        public string PostalCode { get; set; }

        [Column]
        public string Country { get; set; }

        [Column]
        public string Phone { get; set; }

        [Column]
        public string Fax { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
        public ICollection<Orders> Orders { get; set; }

    }
}
