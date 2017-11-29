using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Schema = "dbo", Name ="Products")]
    public class Products
    {
        [PrimaryKey, Identity]
        public int ProductID { get; set; }

        [Column]
        public string ProductName { get; set; }

        [Column]
        public int? SupplierID { get; set; }

        [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
        public Suppliers Suppliers { get; set; }

        [Column]
        public int? CategoryID { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
        public Categories Categories { get; set; }

        [Column]
        public string QuantityPerUnit { get; set; }

        [Column]
        public decimal? UnitPrice { get; set; }

        [Column]
        public short? UnitsInStock { get; set; }

        [Column]
        public short? UnitsOnOrder { get; set; }

        [Column]
        public short? ReorderLevel { get; set; }

        [Column]
        public bool Discontinued { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
