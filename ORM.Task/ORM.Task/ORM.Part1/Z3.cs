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
            //Console.WriteLine("Z3.1");
            //using (var db = new DbNorthwind())
            //{
            //    Employees e = new Employees()
            //    {
            //        FirstName = "TBobby",
            //        LastName = "TMask",
            //        Title = null,
            //        TitleOfCourtesy = null,
            //        BirthDate = DateTime.Today,
            //        HireDate = DateTime.Today,
            //        Address = null,
            //        City = null,
            //        Region = null,
            //        PostalCode = null,
            //        Country = null,
            //        HomePhone = null,
            //        Extension = null,
            //        Photo = null,
            //        Notes = null
            //    };
            //    var employeeID = Convert.ToInt32(db.InsertWithIdentity(e));
            //    Territories territory = db.Territories.SingleOrDefault(t => t.TerritoryDescription == "Cary");
            //    var et = new EmlpoyeeTerritories()
            //    {
            //        EmployeeID = employeeID,
            //        TerritoryID = territory.TerritoryID.ToString()
            //    };
            //    db.InsertWithIdentity(et);
            //}
            //Console.WriteLine("Success!");
            //Z3.2
            //Console.WriteLine("Z3.2");
            //using (var db = new DbNorthwind())
            //{
            //    Random rnd = new Random();
            //    db.Products.Where(p => p.ProductID == 5).Set(p => p.CategoryID, rnd.Next(1, 9)).Update();
            //    Console.WriteLine("Success");
            //}
            //Z3.3
            //Console.WriteLine("Z3.3");
            //using (var db=new DbNorthwind())
            //{
            //    var category = new Categories()
            //    {
            //        CategoryName = "New category",
            //        Description = "New cat",
            //        Picture = null
            //    };
            //    var newCatID = Convert.ToInt32(db.InsertWithIdentity(category));
            //    var supplier = new Suppliers()
            //    {
            //        CompanyName = "NewCompSup",
            //        ContactName = "NewContactName"
            //    };
            //    var newSupID = Convert.ToInt32(db.InsertWithIdentity(supplier));
            //    var p1=new Products()
            //    {
            //        ProductName = "new Product1",
            //        CategoryID = newCatID,
            //        SupplierID = newSupID

            //    };
            //    var p2 = new Products()
            //    {
            //        ProductName = "new Product2",
            //        CategoryID = newCatID,
            //        SupplierID = newSupID
            //    };
            //    db.Insert(p1);
            //    db.Insert(p2);
            //}
            //Console.WriteLine("Success");
            //Z3.4 TODO
            //Console.WriteLine("Z3.4");
            using (var db = new DbNorthwind())
            {
                var q1 = db.OrderDetails.
                    GroupJoin(db.Orders.Where(o => o.ShippedDate == null).Select(c => c), p => p.OrderID, k => k.OrderID, (pk, kp) => new OrderDetails()
                    {
                        ProductID = pk.ProductID,
                        OrderID = pk.OrderID,
                        Quantity = pk.Quantity,
                        UnitPrice = pk.UnitPrice,
                        Discount = pk.Discount
                    });
                //var res1 = q1.ToList();
                //var query = from o in db.Orders
                //            where o.ShippedDate == null
                //            from od in db.OrderDetails.InnerJoin(ood => ood.OrderID == o.OrderID && o.OrderID == 11077)
                //            select new { od.ProductID, od.OrderID, o.ShippedDate, order = o.OrderID };
                //var res = query.ToList();
                //Random rnd = new Random();
                //foreach (var o in query)
                //{
                //    using (var db1 = new DbNorthwind())
                //    {
                //        db1.OrderDetails.Update(t => new OrderDetails()
                //        {
                //            OrderID = o.OrderID,
                //            Quantity = o.Quantity,
                //            Discount = o.Discount,
                //            UnitPrice = o.UnitPrice
                //        });
                //    }
                //}
                Console.WriteLine("Success");
            }

        }
    }
}
