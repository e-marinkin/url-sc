namespace WebParser.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using HtmlAgilityPack;

    public class FlowersHtmlPageParser : IHtmlPageParser
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

            var urls = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                                    .Where(l => !l.Attributes["href"].Value.Contains("#")
                                            && !l.Attributes["href"].Value.Contains("mailto")
                                            && !l.Attributes["href"].Value.Contains("callto"))
                                    .Select(l => !l.Attributes["href"].Value.StartsWith("https://www.flowerstation.co.uk")
                                            ? $"https://www.flowerstation.co.uk{l.Attributes["href"].Value}"
                                            : l.Attributes["href"].Value);
            
            _allUrls.UnionWith(urls);
            _traversingUrls.UnionWith(urls);
        }
    }
}