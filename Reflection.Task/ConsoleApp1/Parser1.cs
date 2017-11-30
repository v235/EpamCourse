using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.DAL;
using ConsoleApp1.Logger;
using MyCustomIoC;

namespace ConsoleApp1
{
    class Parser1
    {
        private readonly IRepository repository;
        private readonly Loger loger;

        //For Test
        //public Parser1()
        //{
        //    repository=new Repository();
        //    loger = new Loger();
        //}
        public Parser1(IRepository repository, Loger loger)
        {
            this.repository = repository;
            this.loger = loger;
        }
        public void parseData()
        {
            loger.AddLog("Data before parse:" + repository.GetData());
            var result = repository.GetData().Split(new char[] {','});
            Console.WriteLine("Separeted string:");
            foreach (var r in result)
            {
                loger.AddLog(r);
            }
        }


    }
}
