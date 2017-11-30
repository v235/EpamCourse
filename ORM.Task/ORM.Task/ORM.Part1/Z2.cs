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
                var query = db.Products.LoadWith(c => c.Categories).LoadWith(s => s.Suppliers).Select(p =>
                    new { p.ProductID, p.ProductName, p.Categories.CategoryName, p.Suppliers.ContactName });
                foreach (var p in query)
                {
                    Console.WriteLine("{0}|{1}|{2}|{3}", p.ProductID, p.ProductName, p.CategoryName, p.ContactName);
                }
            }
            //Z2.2
            Console.WriteLine("Z2.2");
            using (var db = new DbNorthwind())
            {
                var query = db.Employees.Select(e =>
                    new { e.EmployeeID, EmployeeName = e.FirstName + e.LastName, e.Region });
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
                var q = db.Orders.LoadWith(e => e.Employees).LoadWith(s => s.Shippers).GroupBy(g =>
                    new
                    {
                        g.EmployeeID,
                        g.Employees.FirstName,
                        g.Employees.LastName,
                        g.Shippers.CompanyName
                    }).OrderBy(r => r.Key.FirstName);
                foreach (var e in q)
                {
                    Console.WriteLine("{0}|{1}|{2}", e.Key.FirstName + " " + e.Key.LastName, e.Key.CompanyName, e.Count());
                }
            }
        }
    }
}
