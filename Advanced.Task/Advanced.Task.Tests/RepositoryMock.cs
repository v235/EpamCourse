using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.Task.Data;

namespace Advanced.Task.Tests
{
    class RepositoryMock : IRepository
    {
        public IEnumerable<DirectoryInfo> GetDirectorys(string path)
        {
            return new DirectoryInfo(path).GetDirectories("*.*", SearchOption.AllDirectories);
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.*",SearchOption.AllDirectories);
        }
    }
}
