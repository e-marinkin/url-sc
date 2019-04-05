namespace WebParser.Core
{
    using System;
    using System.Collections.Generic;

    public class HtmlPageParserProvider
    {
        private static readonly Dictionary<string,Type> _parsers = new Dictionary<string, Type>{
            {"https://www.flowerstation.co.uk", typeof(FlowersHtmlPageParser)}                
        };

        public static IHtmlPageParser GetHtmlPageParser(string basedUrl)
        {
            if(string.IsNullOrWhiteSpace(basedUrl))
                throw new ArgumentNullException("URL is empty");

            // If the passed url the same
            if(_parsers.ContainsKey(basedUrl))
            {
                return (IHtmlPageParser)Activator.CreateInstance(_parsers[basedUrl]);
            } 
            // Try to find by its domain
            else 
            {
                var uri = new Uri(basedUrl);
                string domain = uri.GetLeftPart(UriPartial.Authority);
                if(!string.IsNullOrWhiteSpace(domain) && _parsers.ContainsKey(domain))
                {
                    return (IHtmlPageParser)Activator.CreateInstance(_parsers[domain]);
                }
            }

            return new DefaultHtmlPageParser();
        }
    }
}