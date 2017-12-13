using System;
using System.Collections.Generic;
using WebLib;

namespace HTTP.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            SiteDownloader sd = new SiteDownloader();
            //1
            //sd.Download(@"D:\5", "https://www.tut.by/");
            //2
            //List<string> filter = new List<string>() { "jpg", "png", "git" };
            //sd.Download(@"D:\5", "https://www.tut.by/", filter);
            //3
            Dictionary<string, bool> linksRules = new Dictionary<string, bool>();
            linksRules.Add("AllSearch", false);
            linksRules.Add("DomainSearch", true);
            linksRules.Add("ParentSearch", false);
            sd.Download(@"D:\5", "https://www.tut.by/", linksRules, 1);

            Console.ReadKey();
        }
    }
}
