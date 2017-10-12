using System;
using System.IO;

namespace Advanced.Task.BL
{
    public class EventsProgressArgs:EventArgs
    {
        public string Message { get;}
        public string FilterParam { get; }
        public FileInfo File { get; internal set; }
        public DirectoryInfo Dir { get; internal set; }
        public FileSystemVisitorContext FsContext { get; set; }
        public EventsProgressArgs(string message)
        {
            Message = message;
        }
    }
}
