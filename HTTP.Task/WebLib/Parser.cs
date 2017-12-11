using System;
using System.IO;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace WebLib
{
    internal class Parser
    {
        private string htmlContent;
        private HtmlDocument htmlSnippet;
        public Parser(string htmlContent)
        {
            this.htmlContent = htmlContent;
            htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(htmlContent);
        }

        public IEnumerable<string> GetAllLinksFromHTML()
        {
            return htmlSnippet.DocumentNode.SelectNodes("//a[@href]")
                .SelectMany(a => a.Attributes
                .Where(h => h.Name == "href" && (h.Value.StartsWith("http")
                || h.Value.StartsWith("https")))).Distinct().Select(l => l.Value);
        }
        public Dictionary<string, string> GetAllImgFromHTML(string baseURL)
        {
            var res = htmlSnippet.DocumentNode.SelectNodes("//img")
                  .Select(g => g.GetAttributeValue("src", "")).Distinct();
            return LinkHelper(res, baseURL);
        }
        public Dictionary<string, string> GetAllJSFromHTML(string baseURL)
        {
            var res = htmlSnippet.DocumentNode.SelectNodes("//script")
                 .Where(a => a.GetAttributeValue("type", "") == "text/javascript")
                 .Select(g => g.GetAttributeValue("src", "")).Distinct();
            return LinkHelper(res, baseURL);
        }
        public Dictionary<string, string> GetAllCssFromHTML(string baseURL)
        {
            var res = htmlSnippet.DocumentNode.SelectNodes("//link")
                .Where(a => a.GetAttributeValue("rel", "") == "stylesheet")
                .Select(g => g.GetAttributeValue("href", "")).Distinct();
            return LinkHelper(res, baseURL);
        }
        private Dictionary<string, string> LinkHelper(IEnumerable<string> sequence, string baseURL)
        {
            Dictionary<string, string> keyValueTable = new Dictionary<string, string>();
            foreach (var el in sequence)
            {
                if (el.Length > 0)
                {
                    if (el.StartsWith("http") || el.StartsWith("https"))
                    {
                        keyValueTable.Add(el, el);
                    }
                    else
                    {
                        keyValueTable.Add(el, GetBaseUrl(baseURL) + el);
                    }
                }
            }
            return keyValueTable;
        }
        private string GetBaseUrl(string url)
        {
            var splitedURL = url.Split('.');
            return splitedURL[0] + "." + splitedURL[1] + "." + (splitedURL[2].IndexOf('/') != -1 ? splitedURL[2].Substring(0, splitedURL[2].IndexOf('/')) : splitedURL[2]);
        }
    }
}
