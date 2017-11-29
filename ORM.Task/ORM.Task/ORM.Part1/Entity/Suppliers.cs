using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name = "Suppliers")]
    public class Suppliers
    {
        [PrimaryKey, Identity]
        public int SupplierID { get; set; }

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

        [Column]
        public string HomePage { get; set; }

        public IEnumerable<Products> Products { get; set; }

        public Suppliers()
        {
            Products=new List<Products>();
        }
    }
}
