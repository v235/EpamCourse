using LinqToDB;
using ORM.Part1.Entity;

namespace ORM.Part1.DBContext
{
    public class DbNorthwind:LinqToDB.Data.DataConnection
    {
        public DbNorthwind(string connectionString) : base(connectionString)
        {
            
        }
        public ITable<Categories> Categories { get { return GetTable<Categories>(); } }
    }
}
