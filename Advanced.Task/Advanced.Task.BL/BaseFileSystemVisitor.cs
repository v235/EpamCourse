using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.Task.Data;

namespace Advanced.Task.BL
{
    public class BaseFileSystemVisitor
    {
        public FileSystemVisitor Searcher { get; private set; }
        public BaseFileSystemVisitor()
        {
            Searcher = new FileSystemVisitor(new Repository());
        }
        public BaseFileSystemVisitor(string path)
        {
            Searcher = new FileSystemVisitor(new Repository(), path);
        }
        public BaseFileSystemVisitor(string path, Func<string, string, bool> filter)
        {
            Searcher = new FileSystemVisitor(new Repository(), path,  filter);
        }

    }
}
