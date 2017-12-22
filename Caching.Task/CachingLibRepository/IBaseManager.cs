using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingLibRepository
{
    public interface IBaseManager
    {
         IEnumerable<T> GetAllData<T>() where T : class;
    }
}
