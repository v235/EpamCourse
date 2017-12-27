using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Linq;
using LinqToDB.Common;
using ORM.Part1.DBContext;
using ORM.Part1.Entity;

namespace ORM.Part1
{
    public class Z3
    {
        public void RunZ3()
        {
            //Z3.1
            Console.WriteLine("Z3.1");
            using (var db = new DbNorthwind())
            {
                Employees e = new Employees()
                {
                    FirstName = "TBobby",
                    LastName = "TMask",
                    Title = null,
                    TitleOfCourtesy = null,
                    BirthDate = DateTime.Today,
                    HireDate = DateTime.Today,
                    Address = null,
                    City = null,
                    Region = null,
                    PostalCode = null,
                    Country = null,
                    HomePhone = null,
                    Extension = null,
                    Photo = null,
                    Notes = null,
                    ReportsTo = 1
                };
                var employeeID = Convert.ToInt32(db.InsertWithIdentity(e));
                Territories territory = db.Territories.SingleOrDefault(t => t.TerritoryDescription == "Cary");
                var et = new EmlpoyeeTerritories()
                {
                    EmployeeID = employeeID,
                    TerritoryID = territory.TerritoryID.ToString()
                };
                db.InsertWithIdentity(et);
            }
            Console.WriteLine("Success!");
            //Z3.2
            Console.WriteLine("Z3.2");
            using (var db = new DbNorthwind())
            {
                Random rnd = new Random();
                db.Products.Where(p => p.ProductID == 5).Set(p => p.CategoryID, rnd.Next(1, 9)).Update();
                Console.WriteLine("Success");
            }
            //Z3.3
            Console.WriteLine("Z3.3");
            using (var db = new DbNorthwind())
            {
                var category = new Categories()
                {
                    CategoryName = "New category",
                    Description = "New cat",
                    Picture = null
                };
                var newCatID = Convert.ToInt32(db.InsertWithIdentity(category));
                var supplier = new Suppliers()
                {
                    CompanyName = "NewCompSup",
                    ContactName = "NewContactName"
                };
                var newSupID = Convert.ToInt32(db.InsertWithIdentity(supplier));
                var p1 = new Products()
                {
                    ProductName = "new Product1",
                    CategoryID = newCatID,
                    SupplierID = newSupID

                };
                var p2 = new Products()
                {
                    ProductName = "new Product2",
                    CategoryID = newCatID,
                    SupplierID = newSupID
                };
                db.Insert(p1);
                db.Insert(p2);
            }
            Console.WriteLine("Success");
            //Z3.4
            Console.WriteLine("Z3.4");
            using (var db = new DbNorthwind())
            {
                List<OrderDetails> newOD = new List<OrderDetails>();
                var query = db.OrderDetails.LoadWith(t => t.Products.Categories).LoadWith(t => t.Orders)
                    .Where(o => o.Orders.ShippedDate == null).ToArray();
                var groupQuery = query.GroupBy(g => g.OrderID);
                Console.WriteLine();
                foreach (var g in groupQuery)
                {
                    var idFilter = g.Select(i => i.ProductID).ToList();
                    Console.WriteLine("OrderID is:{0}", g.Key);
                    foreach (var p in g)
                    {
                        Console.WriteLine("|Product:{0}|Category:{1}|", p.Products.ProductName,
                            p.Products.Categories.CategoryName);
                        var newProductID = db.Products.LoadWith(t => t.Categories).Where(c =>
                            c.ProductID != p.ProductID && c.CategoryID == p.Products.CategoryID).Select(c => c.ProductID).ToArray().Except(idFilter).FirstOrDefault();
                        idFilter.Add(newProductID);
                        newOD.Add(new OrderDetails()
                        {
                            OrderID = p.OrderID,
                            ProductID = newProductID,
                            Quantity = p.Quantity,
                            UnitPrice = p.UnitPrice,
                            Discount = p.Discount
                        });
                    }
                }
                var newODArr = newOD.ToArray();
                for (int i = 0; i < query.Length; i++)
                {
                    if(newODArr[i].ProductID!=0)
                    db.OrderDetails.Where(od => od.OrderID == query[i].OrderID && od.ProductID == query[i].ProductID).Set(od => od.ProductID, newODArr[i].ProductID).Update();
                }
                Console.WriteLine("Success");
            }
        }
    }
}
