using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Task.Entity;

namespace XML.Task.DAL
{
    public class NewspapersRepository : IRepository<Newspaper>
    {
        public IEnumerable<Newspaper> GetData()
        {
            return new[]
            {
                new Newspaper()
                {
                    Name = "USA Today",
                    PublishCity = "Virginia",
                    PublishingHouse = "Gannett's",
                    PublishYear = 1987,
                    PageCount = 37,
                    Comment = "Circulation of 1,021,638",
                    SerialNumber = 1,
                    Date = DateTime.Parse("2008-05-01 7:34:42Z"),
                    ISSN = "0734-7456"
                },
                new Newspaper()
                {
                    Name = "Daily News ",
                    PublishCity = "LasVegas",
                    PublishingHouse = "Gannett's",
                    PublishYear = 1954,
                    PageCount = 37,
                    Comment = "Circulation of 1,021,638",
                    SerialNumber = 1,
                    Date = DateTime.Parse("2012-05-01 7:34:42Z"),
                    ISSN = "1300-0721"
                }
            };
        }
    }
}
