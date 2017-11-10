using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Composition;


namespace IoCSample
{
    [ImportConstructor]
    public class CustomerBLL
    {
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            Console.WriteLine("1");
        }
    }
    [ImportConstructor]
    public class CustomerBL3L
    {
        public CustomerBL3L(ICustomerDAL dal, Logger logger)
        {
            Console.WriteLine("2");
        }
    }
    public class CustomerBLL2
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }
        [Import]
        public Logger logger { get; set; }
    }

    public class CustomerBLL4
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }
        [Import]
        public Logger logger { get; set; }
    }
}
