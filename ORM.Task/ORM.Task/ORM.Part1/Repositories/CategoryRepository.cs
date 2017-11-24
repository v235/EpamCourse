using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Part1.DBContext;
using ORM.Part1.Entity;

namespace ORM.Part1.Repositories
{
    public class CategoryRepository
    {
        private readonly DbNorthwind db;

        public CategoryRepository(string connectionString)
        {
            db = new DbNorthwind(connectionString);
        }

        public IEnumerable<Categories> GetAllCategory()
        {
            return db.Categories.Select(c => c);
        }
    }
}
