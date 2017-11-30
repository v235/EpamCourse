using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name= "Employees")]
    public class Employees
    {
        [PrimaryKey, Identity]
        public int EmployeeID { get; set; }
        
        [Column]
        public string LastName { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string TitleOfCourtesy { get; set; }

        [Column]
        public DateTime BirthDate { get; set; }

        [Column]
        public DateTime HireDate { get; set; }

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
        public string HomePhone { get; set; }

        [Column]
        public string Extension { get; set; }

        [Column]
        public byte[] Photo { get; set; }

        [Column]
        public string Notes { get; set; }

        [Column]
        public int ReportsTo { get; set; }

        [Column]
        public string PhotoPath { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
        public ICollection<Orders> Orders { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public virtual ICollection<Employees> Employee { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public virtual Employees Employee1 { get; set; }

    }
}
