using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table(Name = "Categories")]
    public class Categories
    {
        [PrimaryKey, Identity]
        public int CategoryID { get; set; }

        [Column]
        public string CategoryName { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }

    }
}
