using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JScript;
using ORM.Part1;
using ORM.Part1.DBContext;

namespace ORM.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //Z2 z2=new Z2();
            //z2.RunZ2();
            Z3 z3 = new Z3();
            z3.RunZ3();
            Console.ReadKey();
        }
    }
}
