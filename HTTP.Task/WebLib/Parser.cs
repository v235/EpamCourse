using System;
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
        public IEnumerable<string> GetAllLinksFromHTML ()
        {
            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                if (att.Value.StartsWith("https://") || att.Value.StartsWith("http://"))
                {
                    yield return att.Value;
                }
            }
        }
        public IEnumerable<string> GetAllImgFromHTML(string baseUrl)
        {
            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//img"))
            {
                HtmlAttribute att = link.Attributes["src"];
                if (att.Value.StartsWith("https://")|| att.Value.StartsWith("http://"))
                {
                   yield return att.Value;
                }
                else
                {
                    yield return baseUrl+att.Value;
                }
            }
        }
        public IEnumerable<string> GetAllCssFromHTML(string baseUrl)
        {
            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//link"))
            {
                HtmlAttribute att = link.Attributes["rel"];
                if (att.Value == "stylesheet")
                {
                    if (att != null)
                    {
                        if (link.Attributes["href"].Value.StartsWith("https://"))
                        {
                            yield return link.Attributes["href"].Value;
                        }
                        else
                        {
                            yield return baseUrl + link.Attributes["href"].Value;
                        }
                    }
                }
            }
        }
        public IEnumerable<string> GetAllJSFromHTML(string baseUrl)
        {
            List<string> js = new List<string>();
            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//script"))
            {
                HtmlAttribute att = link.Attributes["src"];
                if (att != null)
                {
                    if (att.Value.StartsWith("https://") || att.Value.StartsWith("http://"))
                    {
                        js.Add(att.Value);
                        //yield return att.Value;
                    }
                    else
                    {
                        js.Add(baseUrl + att.Value);
                    }
                    //yield return baseUrl + link.Attributes["src"].Value;
                }
            }
            return js;
        }
    }
}
