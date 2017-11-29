using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name = "Territories")]
    public class Territories
    {
        [PrimaryKey, Identity]
        public int TerritoryID { get; set; }

        [Column]
        public string TerritoryDescription { get; set; }

        [Column]
        public int RegionID { get; set; }
    }
}
