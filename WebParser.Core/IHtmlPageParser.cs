namespace WebParser.Core
{
    using System.Collections.Generic;
    using HtmlAgilityPack;
    public interface IHtmlPageParser
    {
         void Parse(string html);

         IEnumerable<string> GetAllUrls();

         IEnumerable<string> GetTraversingUrls();
    }
}