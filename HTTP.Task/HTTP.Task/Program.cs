using System;
using WebLib;

namespace HTTP.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            SiteDownloader sd = new SiteDownloader();
            sd.Download(@"D:\5", "https://google.com/");
            Console.ReadKey();
        }
    }
}
