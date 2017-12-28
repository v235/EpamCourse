using CustomSerializationLib.Helpers;
using CustomSerializationLib.SerializationHelpers;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;


namespace CustomSerializationLib
{
    public class CustomSerializationProvider
    {
        Northwind dbContext;
        public CustomSerializationProvider()
        {
            dbContext = new Northwind();
        }

        public void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var categories = dbContext.Categories.ToList();
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, dbContext.Products.ToList())), true);

            var r = tester.SerializeAndDeserialize(categories);
        }

        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var t = (dbContext as IObjectContextAdapter).ObjectContext;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
            var products = dbContext.Products.ToList();

            foreach(var p in products)
            {
                t.LoadProperty(p, f => f.Category);
                t.LoadProperty(p, f => f.Supplier);
                t.LoadProperty(p, f => f.Order_Details);
            }

            tester.SerializeAndDeserialize(products);
        }

        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var t = (dbContext as IObjectContextAdapter).ObjectContext;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All), int.MaxValue, false, System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, new OrderDetailSurrogateSelector()), true);
            var orderDetails = dbContext.Order_Details.ToList();

            foreach (var od in orderDetails)
            {
                t.LoadProperty(od, f => f.Order);
                t.LoadProperty(od, f => f.Product);
            }

            tester.SerializeAndDeserialize(orderDetails);
        }

        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>), new DataContractSerializerSettings { DataContractSurrogate = new OrderSerializationSurrogate() }), true);
            var orders = dbContext.Orders.ToList();

            var r = tester.SerializeAndDeserialize(orders);
        }
    }
}
