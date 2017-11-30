using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCustomIoC;

namespace ConsoleApp1.Logger
{
    [Export]
    public class Loger 
    {
        public void AddLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}
