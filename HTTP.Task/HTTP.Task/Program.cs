using System;
using WebLib;

namespace HTTP.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            SiteDownloader sd = new SiteDownloader();
            sd.Download(@"D:\5", "https://www.epam.com/");
            //Run();
            //Console.WriteLine("Hit ENTER to exit...");
            //Console.ReadLine();

        }

        //static async void Run()
        //{
        //    //HttpClient client = new HttpClient();
        //    //HttpResponseMessage response = await client.GetAsync(_address);
        //    //response.EnsureSuccessStatusCode();
        //    //HttpContent responseContent = response.Content;
        //    //var json = await responseContent.ReadAsStringAsync();
        //}
    }
}
