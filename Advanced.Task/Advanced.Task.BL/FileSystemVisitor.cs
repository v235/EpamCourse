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
        private readonly Func<string, string, bool> filter;
        private readonly IRepository data;

        // events
        public event EventHandler<EventsProgressArgs> OnStart=delegate {};
        public event EventHandler<EventsProgressArgs> OnFinish=delegate {};
        public event EventHandler<EventsProgressArgs> OnFileFinded = delegate { };
        public event EventHandler<EventsProgressArgs> OnDirectoryFinded = delegate { };
        public event EventHandler<EventsProgressArgs> FilterFileFinded = delegate { };
        public event EventHandler<EventsProgressArgs> FilterDirectoryFinded = delegate { };
        //

        public FileSystemVisitor(IRepository data)
        {
            //path = @".";
            path = @"D:\MyJS2";
            this.filter = (name, filter) => name.Contains(filter);
            this.data = data;
        }
        public FileSystemVisitor(IRepository data, string path)
        {
            this.path = path;
            this.filter = (name, filter) => name.Contains(filter);
            this.data = data;
        }
        public FileSystemVisitor(IRepository data, string path, Func<string, string, bool> filter)
        {
            this.path = path;
            this.filter = filter;
            this.data = data;
        }

        public IEnumerable<FileInfo> GetFiles(string filterParam = null)
        {
            OnStart(this, new EventsProgressArgs("Start file search"));
            FileSystemVisitorContext fscontext = new FileSystemVisitorContext();
            foreach (var file in data.GetFiles(path))
            {
                OnFileFinded(this, new EventsProgressArgs("FileFinded: ") { File = file, FsContext = fscontext });
                if (fscontext.IsCancel)
                    yield break;
                if (!string.IsNullOrEmpty(filterParam))
                {
                    OnFilterFileFinded(
                        new EventsProgressArgs("FilteredFileFinded: ") {File = file, FsContext = fscontext});
                    if (fscontext.IsItemPassFilter(file.FullName, filterParam, filter))
                    {
                        yield return fscontext.CheckIsItemExcluded(file);
                    }
                }
                else
                {
                    yield return fscontext.CheckIsItemExcluded(file);
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
            foreach (var dir in data.GetDirectorys(path))
            {
                OnDirectoryFinded(this, new EventsProgressArgs("DirFinded: ") { Dir = dir, FsContext = fscontext });
                if (!string.IsNullOrEmpty(filterParam))
                {
                    OnFilterDirectoryFinded(new EventsProgressArgs("FilteredDirFinded: ") { Dir = dir, FsContext = fscontext });
                    if (fscontext.IsCancel)
                        yield break;
                    if (fscontext.IsItemPassFilter( dir.FullName, filterParam, filter))
                    {
                        yield return fscontext.CheckIsItemExcluded(dir);
                    }
                }
                else
                {
                    yield return fscontext.CheckIsItemExcluded(dir);
                }
            }
            OnFinish(this, new EventsProgressArgs("Finish Dir search"));
        }
        protected virtual void OnFilterDirectoryFinded(EventsProgressArgs e)
        {
            FilterFileFinded?.Invoke(this, e);
        }
    }
}
