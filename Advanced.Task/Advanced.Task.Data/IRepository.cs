using System.Collections.Generic;
using System.IO;

namespace Advanced.Task.Data
{
    public interface IRepository
    {
        IEnumerable<FileInfo> GetFiles(string path, string filterParam);
        IEnumerable<DirectoryInfo> GetDirectorys(string path, string filterParam);
    }
}
