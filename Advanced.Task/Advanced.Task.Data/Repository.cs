using System;
using System.Collections.Generic;
using System.IO;

namespace Advanced.Task.Data
{
    public class Repository : IRepository
    {
        public IEnumerable<DirectoryInfo> GetDirectorys(string path)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(path))
            {
                throw new ArgumentException();
            }
            dirs.Push(path);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                IEnumerable<string> subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                    DirectoryInfo di = new System.IO.DirectoryInfo(str);
                    yield return di;
                }
            }
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(path))
            {
                throw new ArgumentException();
            }
            dirs.Push(path);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                IEnumerable<string> files;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                foreach (var file in files)
                {
                    FileInfo fi = new System.IO.FileInfo(file);
                    yield return fi;
                }
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }
    }
}
