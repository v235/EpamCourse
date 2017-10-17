using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advanced.Task.Data;

namespace Advanced.Task.BL
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly string path;
        private readonly Func<string, bool> filter;
        private readonly IRepository data;

        // events
        public event EventHandler<EventsProgressArgs> OnStart=delegate {};
        public event EventHandler<EventsProgressArgs> OnFinish=delegate {};
        public event EventHandler<EventsProgressArgs> OnFileFinded = delegate { };
        public event EventHandler<EventsProgressArgs> OnDirectoryFinded = delegate { };
        public event EventHandler<EventsProgressArgs> FilterFileFinded ;
        public event EventHandler<EventsProgressArgs> FilterDirectoryFinded;
        //

        public FileSystemVisitor(IRepository data)
        {
            path = @".";
            this.filter = (name) => name.Length > 5;
            this.data = data;
        }
        public FileSystemVisitor(IRepository data, string path)
        {
            this.path = path;
            this.filter = (name) => name.Length > 5;
            this.data = data;
        }
        public FileSystemVisitor(IRepository data, string path, Func<string, bool> filter)
        {
            this.path = path;
            this.filter = filter;
            this.data = data;
        }

        public IEnumerable<FileInfo> GetFiles(string filterParam = null)
        {
            OnStart(this, new EventsProgressArgs("Start file search"));
            FileSystemVisitorContext fscontext = new FileSystemVisitorContext();
                foreach (var file in data.GetFiles(path, filterParam))
                {

                    OnFileFinded(this, new EventsProgressArgs("FileFinded: ") {File = file, FsContext = fscontext});
                    if (fscontext.IsCancel)
                        yield break;
                        OnFilterFileFinded(
                            new EventsProgressArgs("FilteredFileFinded: ") {File = file, FsContext = fscontext});
                        if (fscontext.IsItemPassFilter(file.Name, filter))
                        {
                            if (fscontext.CheckIsItemExcluded(file.FullName))
                            {
                                yield return null;
                            }
                            else
                            {
                                yield return file;
                            }
                        }
                }
            OnFinish(this, new EventsProgressArgs("Finish file search"));
        }

        protected virtual void OnFilterFileFinded(EventsProgressArgs e)
        {
            FilterFileFinded?.Invoke(this, e);
        }
        public IEnumerable<DirectoryInfo> GetDirs(string filterParam = null)
        {
            OnStart(this, new EventsProgressArgs("Start Dir search"));
            FileSystemVisitorContext fscontext = new FileSystemVisitorContext();
            foreach (var dir in data.GetDirectorys(path, filterParam))
            {
                OnDirectoryFinded(this, new EventsProgressArgs("DirFinded: ") { Dir = dir, FsContext = fscontext });
                    OnFilterDirectoryFinded(new EventsProgressArgs("FilteredDirFinded: ") { Dir = dir, FsContext = fscontext });
                    if (fscontext.IsCancel)
                        yield break;
                    if (fscontext.IsItemPassFilter( dir.FullName, filter))
                    {
                        if(fscontext.CheckIsItemExcluded(dir.FullName))
                        {
                            yield return null;
                        }
                        else
                        {
                            yield return dir;
                        }
                    }
            }
            OnFinish(this, new EventsProgressArgs("Finish Dir search"));
        }
        protected virtual void OnFilterDirectoryFinded(EventsProgressArgs e)
        {
            FilterDirectoryFinded?.Invoke(this, e);
        }
    }
}
