using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCustomIoC;

namespace ConsoleApp1.DAL
{
    [Export(typeof(IRepository))]
    public class Repository : IRepository
    {
        public string GetData()
        {
            return File.ReadAllText("Data.txt");
        }
    }
}
