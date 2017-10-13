using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using Advanced.Task.BL;

namespace Advanced.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter search path");
            string path = Console.ReadLine();
            BaseFileSystemVisitor fsVisitor;
            if (string.IsNullOrEmpty(path))
            {
                fsVisitor = new BaseFileSystemVisitor();
            }
            else
            {
                fsVisitor = new BaseFileSystemVisitor(path, (name, filter) => name.Contains(filter));
            }
            fsVisitor.Searcher.OnStart += FsVisitor1_OnStart;
            fsVisitor.Searcher.OnFinish += FsVisitor1_OnFinish;
            fsVisitor.Searcher.OnFileFinded += FsVisitor1_OnFileFinded;
            fsVisitor.Searcher.OnDirectoryFinded += FsVisitor1_OnDirectoryFinded;
            fsVisitor.Searcher.FilterFileFinded += FsVisitor1_OnFilterFileFinded;
            fsVisitor.Searcher.FilterFileFinded += FsVisitor_FilterFileFinded;
            fsVisitor.Searcher.FilterDirectoryFinded += FsVisitor1_OnFilterDirectoryFinded;

            foreach (var file in fsVisitor.Searcher.GetFiles("html"))
            {
                if (file != null)
                {
                    Console.WriteLine(file);
                }
            }

            foreach (var dir in fsVisitor.Searcher.GetDirs())
            {
                if (dir != null)
                {
                    Console.WriteLine(dir);
                }
            }

            Console.ReadKey();
        }

        private static void FsVisitor_FilterFileFinded(object sender, EventsProgressArgs e)
        {
            //some logic
        }

        private static void FsVisitor1_OnFilterDirectoryFinded(object sender, EventsProgressArgs e)
        {
            //some logic
            //if (e.Dir != null)
            //{
            //    //i++;
            //    //if (i == 5)
            //    //    e.FsContext.CancelSearch();
            //if (e.Dir.Name.Contains("images"))
            //    e.FsContext.ExcludeItem();
    }

        private static void FsVisitor1_OnFilterFileFinded(object sender, EventsProgressArgs e)
        {
            //some logic
            //if (e.File != null)
            //{
            //    //i++;
            //    //if (i == 5)
            //    //    e.FsContext.CancelSearch();
            //    if (e.File.FullName.Contains("index"))
            //        e.FsContext.ExcludeItem();
            //}
        }

        private static void FsVisitor1_OnDirectoryFinded(object sender, EventsProgressArgs e)
        {
            //some logic
            //if (e.Dir != null)
            //{
            //    //i++;
            //    //if (i == 5)
            //    //    e.FsContext.CancelSearch();
            if (e.Dir.FullName.Contains("external"))
                e.FsContext.ExcludeItem();
            //}
        }

        private static void FsVisitor1_OnFileFinded(object sender, EventsProgressArgs e)
        {
            //some logic
            //if (e.File != null)
            //{
            //    Console.WriteLine("{0}{1}", e.Message, e.File);
            //}
        }

        private static void FsVisitor1_OnFinish(object sender, EventsProgressArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void FsVisitor1_OnStart(object sender, EventsProgressArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
