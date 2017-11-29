using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name= "EmployeeTerritories")]
    public class EmlpoyeeTerritories
    {
        [PrimaryKey]
        public int EmployeeID { get; set; }

        [PrimaryKey]
        public string TerritoryID { get; set; }
    }
}
