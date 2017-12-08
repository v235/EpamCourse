using System;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WebLib
{

    public class SiteDownloader
    {
        private int stopLevel = 0;
        private string baseUrl;
        public void Download(string downloadPath, string siteUrl, int downloadLevel = 2)
        {
            stopLevel = downloadLevel;
            baseUrl = siteUrl;
            int currentLevel = 0;
            DownloadSite(downloadPath, siteUrl, currentLevel);
        }

        #region page download
        private void DownloadSite(string downloadPath, string siteUrl, int currentLevel)
        {
            var content = GetContent(siteUrl);
            if (content != null)
            {
                //html
                DownloadHTMLContentToDisk(content.ReadAsStreamAsync().Result, downloadPath, siteUrl);
                Parser parser = new Parser(content.ReadAsStringAsync().Result);
                //img
                DownloadImgContentToDisk(downloadPath, siteUrl, parser);
                ////css
                DownloadCssContentToDisk(downloadPath, siteUrl, parser);
                //js
                DownloadJSContentToDisk(downloadPath, siteUrl, parser);
                //Links
                DownloadContentByLinks(GetSavePath(downloadPath, siteUrl), currentLevel, content.ReadAsStringAsync().Result, parser);
            }
        }
        private void DownloadHTMLContentToDisk(Stream content, string downloadPath, string url)
        {
            CreateDownloadFolder(Path.Combine(GetSavePath(downloadPath, url)));
            WriteFromStream(Path.Combine(GetSavePath(downloadPath, url),"index.html"), content);
        }
        private void DownloadImgContentToDisk(string downloadPath, string siteUrl, Parser parser)
        {
            Dictionary<string, string> pathHolder = new Dictionary<string, string>();
            downloadPath = Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl)));
            foreach (string img in parser.GetAllImgFromHTML(siteUrl))
            {
                if (pathHolder.All(k => k.Key != img.Substring(siteUrl.Length)))
                    {
                    pathHolder.Add(img.Substring(siteUrl.Length), Path.Combine("./", img.Substring(img.LastIndexOf("/") + 1)));
                }
                DownloadDopFiles(img, Path.Combine(downloadPath, img.Substring(img.LastIndexOf("/") + 1)));
            }
            if (pathHolder.Count > 0)
            {
                string content = File.ReadAllText(Path.Combine(downloadPath, "index.html"));
                foreach (var css in pathHolder)
                {
                    content = content.Replace(css.Key, css.Value);
                }
                File.WriteAllText(Path.Combine(downloadPath, "index.html"), content);
            }
        }
        private void DownloadCssContentToDisk(string downloadPath, string siteUrl, Parser parser)
        {
            Dictionary<string, string> pathHolder = new Dictionary<string, string>();
            downloadPath = Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl)));
            foreach (string css in parser.GetAllCssFromHTML(siteUrl))
            {
                if (pathHolder.All(k => k.Key != css.Substring(siteUrl.Length)))
                {
                    pathHolder.Add(css.Substring(siteUrl.Length), Path.Combine("./", css.Substring(0, css.LastIndexOf(".css") + 4).Substring(css.Substring(0, css.LastIndexOf(".css") + 4).LastIndexOf("/") + 1)));
                }
                    DownloadDopFiles(css, Path.Combine(downloadPath, css.Substring(0, css.LastIndexOf(".css") + 4).Substring(css.Substring(0, css.LastIndexOf(".css") + 4).LastIndexOf("/") + 1)));
            }
            if (pathHolder.Count > 0)
            {
                string content = File.ReadAllText(Path.Combine(downloadPath, "index.html"));
                foreach (var css in pathHolder)
                {
                    content = content.Replace(css.Key, css.Value);
                }
                File.WriteAllText(Path.Combine(downloadPath, "index.html"), content);
            }
        }
        private void DownloadJSContentToDisk(string downloadPath, string siteUrl, Parser parser)
        {
            Dictionary<string, string> pathHolder = new Dictionary<string, string>();
            downloadPath = Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl)));
            foreach (string js in parser.GetAllJSFromHTML(baseUrl))
            {
                var r= js.Substring(0, js.LastIndexOf(".js") + 3).Substring(js.Substring(0, js.LastIndexOf(".js") + 3).LastIndexOf("/") + 1);
                if (pathHolder.All(k => k.Key != js.Substring(siteUrl.Length)))
                {
                    pathHolder.Add(js.Substring(siteUrl.Length), Path.Combine("./", js.Substring(0, js.LastIndexOf(".js") + 3).Substring(js.Substring(0, js.LastIndexOf(".js") + 3).LastIndexOf("/") + 1)));
                }
                DownloadDopFiles(js, Path.Combine(downloadPath, js.Substring(0, js.LastIndexOf(".js") + 3).Substring(js.Substring(0, js.LastIndexOf(".js") + 3).LastIndexOf("/") + 1)));
            }
            if (pathHolder.Count > 0)
            {
                string content = File.ReadAllText(Path.Combine(downloadPath, "index.html"));
                foreach (var css in pathHolder)
                {
                    content = content.Replace(css.Key, css.Value);
                }
                File.WriteAllText(Path.Combine(downloadPath, "index.html"), content);
            }
        }
        private void DownloadContentByLinks(string ParentPath, int currentLevel, string HTMLForParse, Parser parser)
        {
            if (currentLevel < stopLevel)
            {
                currentLevel = currentLevel + 1;
                foreach (string l in parser.GetAllLinksFromHTML())
                {
                    DownloadSite(ParentPath, l, currentLevel);
                }
            }
            currentLevel = currentLevel - 1;
        }
        private void DownloadDopFiles(string url, string downloadPath)
        {
            try
            {
                WriteFromStream(downloadPath, GetContent(url).ReadAsStreamAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region help methods
        private string GetSavePath(string downloadPath, string url)
        {
            if (Path.Combine(downloadPath, url.Replace(':', '_').Replace('/', '_').Replace('?', '_')).Length < 248)
                return Path.Combine(downloadPath, url.Replace(':', '_').Replace('/', '_').Replace('?', '_'));
            return Path.Combine(downloadPath, url.Replace(':', '_').Replace('/', '_').Replace('?', '_')).Substring(0, 247);

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
        private void WriteFromStream(string savePath, Stream stream)
        {
            try
            {
                using (Stream outputStream = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void CreateDownloadFolder(string savePath)
        {
            Directory.CreateDirectory(savePath);
        }
        #endregion
    }
}
