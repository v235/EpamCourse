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

        public static FileSystemVisitor CreateSearcher()
        {
            return new FileSystemVisitor(new Repository());
        }
        public static FileSystemVisitor CreateSearcher(string path)
        {
            return new FileSystemVisitor(new Repository(), path);
        }
        public static FileSystemVisitor CreateSearcher(string path, Func<string, bool> filter)
        {
            return new FileSystemVisitor(new Repository(), path, filter);
        }
    }
}
