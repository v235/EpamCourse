using System;
using System.Net.Http;
using System.IO;
using System.Text;

namespace WebLib
{
    public class SiteDownloader
    {

        public void Download(string downloadPath, string siteUrl)
        {
            var content = GetContent(siteUrl);
            if (content != null)
            {
                downloadContentToDisk(content.ReadAsStreamAsync().Result, downloadPath, siteUrl);
                Parser parser = new Parser(content.ReadAsStringAsync().Result);
                //var r = parser.GetAllLinksFromHTML();
                foreach (string link in parser.GetAllLinksFromHTML())
                {
                    Download(GetSavePath(downloadPath, siteUrl), link);
                }
            }
        }
        private void downloadContentToDisk(Stream content, string downloadPath, string url)
        {
            string savePath=GetSavePath(downloadPath, url);
            CreateDownloadFolder(savePath);
            SaveFileStream(savePath, content);

        }
        private string GetSavePath(string downloadPath, string url)
        {
            return Path.Combine(downloadPath, url.Replace(':', '_').Replace('/', '_'));

        }
        private HttpContent GetContent(string link)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(link).Result;

                if (response.IsSuccessStatusCode)
                {
                   return response.Content;
                }
                return null;
            }
        }

        //private void DownloadChilde(string ParentPath, string link)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = client.GetAsync(link).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            downloadContentToDisk(content.ReadAsStreamAsync().Result, downloadPath, siteUrl);
        //            Parser parser = new Parser(content.ReadAsStringAsync().Result);
        //            //CreateDownloadFolder(downloadPath, link);
        //            //SaveFileStream(downloadPath, response.Content.ReadAsStreamAsync().Result);

        //        }
        //    }
        //}

        private void SaveFileStream(string savePath, Stream stream)
        {
            var fileStream = new FileStream(savePath+"\\index.html", FileMode.OpenOrCreate);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }

        private void CreateDownloadFolder(string savePath)
        {
            Directory.CreateDirectory(savePath);
        }
    }
}
