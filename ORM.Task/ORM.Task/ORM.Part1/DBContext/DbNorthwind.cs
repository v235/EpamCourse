using LinqToDB;
using ORM.Part1.Entity;

namespace ORM.Part1.DBContext
{
    public class DbNorthwind:LinqToDB.Data.DataConnection
    {
        public DbNorthwind() : base("Northwind")
        {
            
        }
        public ITable<Categories> Categories { get { return GetTable<Categories>(); } }
        public ITable<Employees> Employees { get { return GetTable<Employees>(); } }
        public ITable<EmlpoyeeTerritories> EmlpoyeeTerritories { get { return GetTable<EmlpoyeeTerritories>(); } }
        public ITable<Territories> Territories { get { return GetTable<Territories>(); } }
        public ITable<Customers> Customers { get { return GetTable<Customers>(); } }
        public ITable<Orders> Orders { get { return GetTable<Orders>(); } }
        public ITable<OrderDetails> OrderDetails { get { return GetTable<OrderDetails>(); } }
        public ITable<Products> Products { get { return GetTable<Products>(); } }
        public ITable<Shippers> Shippers { get { return GetTable<Shippers>(); } }
        public ITable<Suppliers> Suppliers { get { return GetTable<Suppliers>(); } }


    }
}
