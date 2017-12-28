using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using NorthwindLibrary;

namespace CustomSerializationLib.SerializationHelpers
{
    internal class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var od = (Order_Detail)obj;
            info.AddValue("OrderID", od.OrderID);
            info.AddValue("ProductID", od.ProductID);
            info.AddValue("UnitPrice", od.UnitPrice);
            info.AddValue("Quantity", od.Quantity);
            info.AddValue("Discount", od.Discount);
            info.AddValue("Order", od.Order);
            info.AddValue("Product", od.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var od = (Order_Detail)obj;
            od.OrderID = info.GetInt32("OrderID");
            od.ProductID = info.GetInt32("ProductID");
            od.UnitPrice = info.GetDecimal("UnitPrice");
            od.Quantity=info.GetInt16("Quantity");
            od.Discount=(float)info.GetValue("Discount", typeof(float));
            od.Order=(Order)info.GetValue("Order", typeof(Order));
            od.Product=(Product)info.GetValue("Product", typeof(Product));
            return od;
        }
    }
}
