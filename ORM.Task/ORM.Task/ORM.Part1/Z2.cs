using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using ORM.Part1.DBContext;

namespace ORM.Part1
{
    public class Z2
    {
        public void RunZ2()
        {
            //Z2.1
            Console.WriteLine("Z2.1");
            using (var db = new DbNorthwind())
            {
                var query = from c in db.Categories
                            from p in db.Products.InnerJoin(pc => pc.CategoryID == c.CategoryID)
                            join s in db.Suppliers on p.SupplierID equals s.SupplierID
                            select new
                            {
                                ProductID = p.ProductID,
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                SuppliersName = s.ContactName
                            };
                foreach (var p in query)
                {
                    Console.WriteLine("{0}|{1}|{2}|{3}", p.ProductID, p.ProductName, p.CategoryName, p.SuppliersName);
                }
            }
            //Z2.2
            Console.WriteLine("Z2.2");
            using (var db = new DbNorthwind())
            {
                var query = db.Employees.Select(e =>
                    new { EmployeeID = e.EmployeeID, EmployeeName = e.FirstName + e.LastName, Region = e.Region });
                foreach (var e in query)
                {
                    Console.WriteLine("{0}|{1}|{2}", e.EmployeeID, e.EmployeeName, e.Region);
                }
            }
            //Z2.3
            Console.WriteLine("Z2.3");
            using (var db = new DbNorthwind())
            {
                var query = db.Employees.GroupBy(e => e.Region).Select(g =>
                    new { GroupName = g.Key != null ? g.Key : "No region", Count = g.Count() });
                foreach (var e in query)
                {
                    Console.WriteLine("{0}|{1}", e.GroupName, e.Count);
                }
            }
            //Z2.4
            Console.WriteLine("Z2.4");
            using (var db = new DbNorthwind())
            {
                var query = from e in db.Employees
                            from o in db.Orders.InnerJoin(eo => eo.EmployeeID == e.EmployeeID)
                            join s in db.Shippers on o.ShipVia equals s.ShipperID
                            group e.EmployeeID by new { e.FirstName, e.LastName, s.CompanyName }
                    into g
                            select new
                            {
                                EmployeeName = g.Key.FirstName + g.Key.LastName,
                                CompanyName = g.Key.CompanyName,
                                Count = g.Count()
                            };

                foreach (var e in query)
                {
                    Console.WriteLine("{0}|{1}|{2}", e.EmployeeName, e.CompanyName, e.Count);
                }
            }
        }
    }
}
