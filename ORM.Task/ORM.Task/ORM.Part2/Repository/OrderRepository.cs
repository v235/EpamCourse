using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Part2.DBContext;
using ORM.Part2.Entity;

namespace ORM.Part2.Repository
{
    public class OrderRepository
    {
        public void GetOrdersGroupedByCategory()
        {
            using (var context = new NorthwindDB())
            {
                var query = context.Order_Details.Select(od => new
                {
                    od.OrderID,
                    od.Order.Customer.ContactName,
                    od.Product.ProductName,
                    od.Product.Category.CategoryName,
                    od.Product.CategoryID
                }).GroupBy(g => new {g.CategoryID, g.CategoryName}).Select(c =>
                    new
                    {
                        category = c.Key,
                        OrdersDetails = c.Select(o => new {o.OrderID, o.ContactName, o.ProductName,})
                    }).ToList();

                foreach (var c in query)
                {
                    Console.WriteLine("CategoryID:{0}|CategoryName:{1}", c.category.CategoryID,
                        c.category.CategoryName);
                    foreach (var o in c.OrdersDetails)
                    {
                        Console.WriteLine("OrderID:{0}|ContactName:{1}|PruductName:{2}",
                            o.OrderID, o.ContactName, o.ProductName);
                    }
                }
            }
        }
    }
}
