using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JScript;
using ORM.Part1;
using ORM.Part1.DBContext;
using ORM.Part2;

namespace ORM.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //For ORMPart1 uncommit in App.config connectionstring for Part1
            Z2 z2 = new Z2();
            z2.RunZ2();
            //Z3 z3 = new Z3();
            //z3.RunZ3();

            //For ORMPart2 uncommit in App.config connectionstring for Part2
            //Part2Zadanie p2z1 = new Part2Zadanie();
            //p2z1.RunZ1();
            //p2z1.RunZ3();

            Console.ReadKey();
        }
    }
}
