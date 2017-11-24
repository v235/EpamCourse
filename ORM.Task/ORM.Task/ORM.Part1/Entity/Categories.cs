using System;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table(Name = "Categories")]
    public class Categories
    {
        [PrimaryKey, Identity]
        public int CategoryID { get; set; }

        [Column(Name = "CategoryName"), NotNull]
        public string CategoryName { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }

        [Column(Name = "Picture")]
        public string Picture { get; set; }

    }
}
