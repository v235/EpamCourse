using System.Collections.Generic;
using System.IO;

namespace Advanced.Task.Data
{
    public interface IRepository
    {
        IEnumerable<FileInfo> GetFiles(string path);
        IEnumerable<DirectoryInfo> GetDirectorys(string path);
    }
}
