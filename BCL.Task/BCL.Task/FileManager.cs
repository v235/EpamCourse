using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                    if (r.FileAddNumber)
                    {
                        File.Copy(e.FullPath, Path.Combine(r.DestDir, "(" + Directory.GetFiles(r.DestDir).Length + ")" + e.Name));
                    }
                    if (r.FileAddDate)
                    {
                        File.Copy(e.FullPath, Path.Combine(r.DestDir, "(" + File.GetLastAccessTime(e.FullPath).ToString("yy-MM-dd") + ")" + e.Name), true);
                    }
                    if (!r.FileAddNumber && !r.FileAddDate)
                    {
                        File.Copy(e.FullPath, Path.Combine(r.DestDir, e.Name), true);
                    }
                    Console.WriteLine("{0}:{1}", messages.FileMovedTo, r.DestDir);
                    File.Delete(e.FullPath);
                    copyToDefault = false;
                }
            }
            if (copyToDefault && File.Exists(e.FullPath))
            {
                Console.WriteLine("{0}:{1}", messages.CorrectFileFound, messages.No);
                File.Copy(e.FullPath, Path.Combine(config.Default.DefaultPath, e.Name), true);
                Console.WriteLine("{0}:{1}", messages.FileMovedTo, config.Default.DefaultPath);
                File.Delete(e.FullPath);
            }
        }
    }
}
