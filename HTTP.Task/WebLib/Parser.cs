using System.Collections.Generic;
using HtmlAgilityPack;

namespace WebLib
{
    internal class Parser
    {
        private readonly string htmlContent;
        public Parser(string htmlContent)
        {
            this.htmlContent = htmlContent;
        }
        public IEnumerable<string> GetAllLinksFromHTML ()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(htmlContent);

            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }
    }
}
