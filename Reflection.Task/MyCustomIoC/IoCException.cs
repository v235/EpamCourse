using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomIoC
{
    public class IoCException:Exception
    {
        public IoCException() : base("Some exeption")
        {
            
        }
        public IoCException(string message):base(message)
        {
            
        }
    }
}
