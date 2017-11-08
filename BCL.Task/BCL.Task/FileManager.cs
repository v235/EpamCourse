using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BCL.Task.Configuration;
using messages = BCL.Task.Resources.Messages;

namespace BCL.Task
{
    public class FileManager
    {
        private readonly ProgConfigurationSection config;

        public FileManager(ProgConfigurationSection config)
        {
            this.config = config;
        }

        public void StartWatch()
        {
            foreach (DirectoryElement el in config.Dirs)
            {
                var watcher = new FileSystemWatcher(el.Path);
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Filter = "*.*";
                watcher.Changed += Watcher_Changed;
                watcher.EnableRaisingEvents = true;
            }

        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            CultureInfo culture = new CultureInfo(config.Default.Culture);
            Thread.CurrentThread.CurrentUICulture = culture;
            Console.WriteLine("{0}:{1}", messages.Lang, culture.DisplayName);
            Console.WriteLine("{0}:{1} {2}:{3}", messages.NewFileFound, e.Name, messages.CreatedDate,
            File.GetCreationTime(e.FullPath).ToString(culture));
            bool copyToDefault = true;
            foreach (RuleElement r in config.Rules)
            {
                Regex regex = new Regex(r.FileName, RegexOptions.IgnoreCase);
                if (regex.Match(e.Name).Success && File.Exists(e.FullPath))
                {
                    Console.WriteLine("{0}:{1}", messages.CorrectFileFound, messages.Yes);
                    string path = GetFilePathToCopy(r.DestDir, e.Name, e.FullPath, r.FileAddNumber, r.FileAddDate);
                    CopyFile(e.FullPath, path);
                    copyToDefault = false;
                }
            }            
            if (copyToDefault && File.Exists(e.FullPath))
            {
                Console.WriteLine("{0}:{1}", messages.CorrectFileFound, messages.No);
                string path = Path.Combine(config.Default.DefaultPath, e.Name);
                CopyFile(e.FullPath, path);
            }
        }

        private void CopyFile(string copyFrom, string copyTo)
        {
            if (!Directory.Exists(Path.GetDirectoryName(copyTo)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(copyTo));
            }
            File.Copy(copyFrom,copyTo,true);
            Console.WriteLine("{0}:{1}", messages.FileMovedTo, copyTo);
            File.Delete(copyFrom);
        }

        private string GetFilePathToCopy(string destDir, string fileName, string fullPath, bool fileAddNumber,
            bool fileAddDate)
        {
            if (!fileAddDate && !fileAddNumber)
            {
                return Path.Combine(destDir, fileName);
            }
            string path = "";
            if (fileAddDate)
            {
                path += GetDateForFileName(fullPath);
            }
            path += fileName.Substring(0, fileName.LastIndexOf('.'));
            string fileExtension = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));

            if (fileAddNumber)
            {
                path += GetNumberForFileName(destDir, path, fileExtension);
            }
            path += fileExtension;
            return Path.Combine(destDir, path);
        }

        private string GetNumberForFileName(string destDir, string fileName, string fileExtension)
        {
            if (Directory.Exists(destDir))
            {
                var files = Directory.GetFiles(destDir, '*' + fileExtension);
                if (files.Length > 0)
                {
                    int fileNumber = files.Where(f => f.Contains(fileName)).Count();
                    if (fileNumber > 0)
                    {
                        while(File.Exists(Path.Combine(destDir, fileName + "(" + fileNumber + ")" + fileExtension)))
                        {
                            fileNumber++;
                        }
                        return "(" + fileNumber +")";
                    }
                }
                int number = Directory.GetFiles(destDir).Length + 1;
                return "("+ number + ")";
            }
            return "(1)";
        }

        private string GetDateForFileName(string fullPath)
        {
            return File.GetLastAccessTime(fullPath).ToString("yy-MM-dd");
        }

    }
}
