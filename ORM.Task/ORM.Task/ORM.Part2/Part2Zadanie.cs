using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Part2.DBContext;
using ORM.Part2.Migrations;
using ORM.Part2.Repository;

namespace ORM.Part2
{
    public class Part2Zadanie
    {
        public void RunZ1()
        {
            OrderRepository or=new OrderRepository();
            or.GetOrdersGroupedByCategory();
        }

        public void RunZ3()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NorthwindDB, Configuration>());
            using (var db = new NorthwindDB())
            {
                db.Database.Initialize(false);
            }
        }
    }
}
