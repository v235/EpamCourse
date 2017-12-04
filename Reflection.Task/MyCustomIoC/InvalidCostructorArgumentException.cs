using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomIoC
{
    public class InvalidCostructorArgumentException:Exception
    {
        public InvalidCostructorArgumentException() : base("Some exeption")
        {
            
        }
        public InvalidCostructorArgumentException(string message):base(message)
        {
            
        }
    }
}
