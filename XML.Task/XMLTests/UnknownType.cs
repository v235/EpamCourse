using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTests
{
    public class UnknownType:Exception
    {
        public UnknownType() : base("Some exeption")
        {

        }
        public UnknownType(string message):base(message)
        {

        }
    }
}
