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
        internal Dictionary<string,bool> excludedItems=new Dictionary<string, bool>();
        public IReadOnlyDictionary<string, bool> ExcludedItems
        {
            get { return (IReadOnlyDictionary<string, bool>)excludedItems; }
        }


        public void CancelSearch ()
        {
            IsCancel = true;
        }

        internal bool IsItemPassFilter(string name, Func<string, bool> filter)
        {
            return filter(name);
        }

        public void ExcludeItem(string fullName)
        {
            excludedItems.Add(fullName,true);
        }

        internal bool CheckIsItemExcluded(string fullName)
        {
                if (excludedItems.ContainsKey(fullName))
                {
                    return excludedItems[fullName];
                }
            return false;
        }

    }
}
