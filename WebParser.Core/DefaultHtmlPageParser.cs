namespace WebParser.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using HtmlAgilityPack;

    public class DefaultHtmlPageParser : IHtmlPageParser
    {
        private HashSet<string> _allUrls = new HashSet<string>();
        
        private HashSet<string> _traversingUrls = new HashSet<string>();

        public IEnumerable<string> GetAllUrls() => _allUrls;

        public IEnumerable<string> GetTraversingUrls() => _traversingUrls;

        public void Parse(string html)
        {
            if(string.IsNullOrWhiteSpace(html))
                throw new ArgumentNullException("HTML Document is empty.");

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var linkNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            var urls = linkNodes.Select(l => l.Attributes["href"].Value).ToList();

            // By Default all urls are equal urls for traversing
            _allUrls.UnionWith(urls);
            _traversingUrls.UnionWith(urls);
        }
    }
   
}