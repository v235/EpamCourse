using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Task.BL
{
    public class FileSystemVisitorContext
    {
        internal bool IsCancel { get; private set; }
        internal bool IsExcluded { get; private set; }


        public void CancelSearch ()
        {
            IsCancel = true;
        }

        internal bool IsItemPassFilter(string name, string filterParam, Func<string, string, bool> filter)
        {
            return filter(name, filterParam);
        }

        public void ExcludeItem()
        {
            IsExcluded= true;
        }

        internal T CheckIsItemExcluded<T>(T item)
        {
            if (IsExcluded)
            {
                IsExcluded = false;
                return default(T);
            }
            return item;
        }

    }
}
