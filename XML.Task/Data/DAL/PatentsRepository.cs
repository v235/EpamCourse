using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Task.Entity;

namespace XML.Task.DAL
{
    public class PatentsRepository : IRepository<Patent>
    {
        public IEnumerable<Patent> GetData()
        {
            return new[]
            {
                new Patent()
                {
                    Name = "Wal-Mart",
                    Creator = "Martin Dock",
                    Country = "England",
                    SerialNumber = 12345,
                    RequestDate = DateTime.Parse("2005-02-01 7:34:42Z"),
                    PublishDate = DateTime.Parse("2014-08-05 7:34:42Z"),
                    PageCount = 20,
                    Comment = "trade"
                },
                new Patent()
                {
                    Name = "McDonalds",
                    Creator = "Mack Boy",
                    Country = "USA",
                    SerialNumber = 520,
                    RequestDate = DateTime.Parse("1995-02-01 7:34:42Z"),
                    PublishDate = DateTime.Parse("2000-08-05 14:20:42Z"),
                    PageCount = 100,
                    Comment = "food"
                }
            };
        }
    }
}
