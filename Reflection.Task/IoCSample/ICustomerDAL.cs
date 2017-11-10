using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCSample
{
    public interface ICustomerDAL
    {
    }

    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {

    }
}
