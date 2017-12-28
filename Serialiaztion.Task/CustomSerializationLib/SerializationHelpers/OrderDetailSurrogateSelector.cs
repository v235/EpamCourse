using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using NorthwindLibrary;

namespace CustomSerializationLib.SerializationHelpers
{
    internal class OrderDetailSurrogateSelector:SurrogateSelector
    {
        public OrderDetailSurrogateSelector()
        {
            this.AddSurrogate(typeof(Order_Detail), new StreamingContext(StreamingContextStates.All), new OrderDetailSerializationSurrogate());
        }

        
    }
}
