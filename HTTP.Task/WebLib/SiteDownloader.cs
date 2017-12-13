﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebLib
{

    public class SiteDownloader
    {
        private int stopLevel = 0;
        private List<string> contentFilteringRef;
        private Dictionary<string, bool> linkSearchRulesRef;


        public void Download(string downloadPath, string siteUrl, int downloadLevel = 2)
        {
            stopLevel = downloadLevel;
            int currentLevel = 0;
            DownloadSite(downloadPath, siteUrl, currentLevel);
        }

        public void Download(string downloadPath, string siteUrl, List<string> contentFiltering, int downloadLevel = 0)
        {
            contentFilteringRef = contentFiltering;
            Download(downloadPath, siteUrl, downloadLevel);
        }

        public void Download(string downloadPath, string siteUrl, Dictionary<string, bool> linkSearchRules, int downloadLevel = 0)
        {
            linkSearchRulesRef = linkSearchRules;
            Download(downloadPath, siteUrl, downloadLevel);
        }

        #region page download
        private void DownloadSite(string downloadPath, string siteUrl, int currentLevel)
        {
            try
            { 
                var content = GetContent(siteUrl).ReadAsStringAsync().Result;
                if (content != null)
                {
                    Dictionary<string, string> pathHolder = new Dictionary<string, string>();
                    //html
                    var r = Path.Combine(GetSavePath(downloadPath, siteUrl));
                    CreateDownloadFolder(Path.Combine(GetSavePath(downloadPath, siteUrl)));
                    Parser parser = new Parser(content);
                    //img
                    if ((contentFilteringRef != null)&&(contentFilteringRef.Any()))
                    {
                        DownloadContentToDisk(Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl))), parser.GetAllImgFromHTMLWithFilter(siteUrl, contentFilteringRef), ref pathHolder);
                    }
                    else
                    {
                        DownloadContentToDisk(Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl))), parser.GetAllImgFromHTMLWithoutFilter(siteUrl), ref pathHolder);
                    }
                    //css
                    DownloadContentToDisk(Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl))), parser.GetAllCssFromHTML(siteUrl), ref pathHolder);
                    //js
                    DownloadContentToDisk(Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl))), parser.GetAllJSFromHTML(siteUrl), ref pathHolder);
                    //Links
                    DownloadContentByLinks(GetSavePath(downloadPath, siteUrl), siteUrl, currentLevel, parser);
                    //write HTML
                    foreach (var e in pathHolder)
                    {
                        content = content.Replace(e.Key, e.Value);
                    }
                    byte[] byteArray = Encoding.UTF8.GetBytes(content);
                    WriteFromStream(Path.Combine(Path.Combine(GetSavePath(downloadPath, siteUrl + "\\index.html"))), new MemoryStream(byteArray));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void DownloadContentToDisk(string downloadPath, Dictionary<string, string> keyValueTable, ref Dictionary<string, string> pathHolderRef)
        {
            foreach (var el in keyValueTable)
            {
                DownloadDopFiles(el.Value, Path.Combine(downloadPath, GetDopFileName(el.Key)));
                pathHolderRef.Add(el.Key, "./" + GetDopFileName(el.Key));
            }
        }
        private void DownloadContentByLinks(string ParentPath, string baseUrl, int currentLevel, Parser parser)
        {
            if (currentLevel < stopLevel)
            {
                currentLevel = currentLevel + 1;
                foreach (string l in parser.GetAllLinksFromHTML(baseUrl, linkSearchRulesRef))
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
                var content = GetContent(url);
                if (content != null)
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
        private string GetDopFileName(string name)
        {
            if (name.LastIndexOf("?") != -1)
            {
                var res = name.Substring(name.LastIndexOf('/'), name.LastIndexOf("?") - name.LastIndexOf("/") - 1);
                return name.Substring(name.LastIndexOf('/') + 1, name.LastIndexOf("?") - name.LastIndexOf("/") - 1);
            }
            else
            {
                var r1 = name.Substring(name.LastIndexOf('/') + 1);
                return name.Substring(name.LastIndexOf('/') + 1);
            }
        }
        private HttpContent GetContent(string url)
        {
            LinkReport(url);
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "deflate");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-BY,en-US;q=0.9,en;q=0.8");
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                ContentReport(response.StatusCode.ToString());
                return response.Content;
            }
            ContentReport(response.StatusCode.ToString());
            return null;
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
            if(savePath.Length<248)
            {
                Directory.CreateDirectory(savePath);
            }
            else
            {
                Directory.CreateDirectory(savePath.Substring(0,247));
            }

        }
        private void LinkReport(string url)
        {
            Console.WriteLine("Try to GET content from:{0}", url);
        }
        private void ContentReport(string status)
        {
            Console.WriteLine("Result code: {0}", status);
        }
        #endregion
    }
}
