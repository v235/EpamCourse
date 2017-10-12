using System;
using System.Collections.Generic;
using System.IO;

namespace Advanced.Task.BL
{
    public interface IFileSystemVisitor
    {
        IEnumerable<FileInfo> GetFiles(string filterParam);
        IEnumerable<DirectoryInfo> GetDirs(string filterParam);

        //event
        event EventHandler<EventsProgressArgs> OnStart;
        event EventHandler<EventsProgressArgs> OnFinish;
        event EventHandler<EventsProgressArgs> OnFileFinded;
        event EventHandler<EventsProgressArgs> OnDirectoryFinded;
        event EventHandler<EventsProgressArgs> FilterFileFinded;
        event EventHandler<EventsProgressArgs> FilterDirectoryFinded;
    }
}
