using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomIoC
{
    public class DependencyNotRegistered:Exception
    {
        public DependencyNotRegistered() : base("Some exeption")
        {
            
        }
        public DependencyNotRegistered(string message):base(message)
        {
            
        }
    }
}
