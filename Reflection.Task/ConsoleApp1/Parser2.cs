using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.DAL;
using ConsoleApp1.Logger;

namespace ConsoleApp1
{
    class Parser2
    {
        [Import]
        public IRepository Repository { get; set; }
        [Import]
        public Loger Loger { get; set; }

        public void GetFirstWord()
        {
            Loger.AddLog("All data:"+Repository.GetData());
            var result = Repository.GetData()[0];
            Loger.AddLog("First word:"+result);
        }
    }
}
