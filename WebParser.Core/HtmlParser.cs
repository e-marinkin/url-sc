
namespace WebParser.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    ///     Base class to parse a website.
    /// </summary>
    public class HtmlParser : IHtmlParser
    {
        private readonly GlobalStorageInMemory _storage;

        /// <summary>
        ///     Ctor with a seed URL to start.
        /// </summary>
        /// <param name="baseUrl">Url</param>
        public HtmlParser(string baseUrl)
        {
            var uri = new Uri(baseUrl);
            _storage = new GlobalStorageInMemory(uri.GetLeftPart(UriPartial.Authority));
            _storage.InitialCapacity = 10;
        }

        public async Task<IEnumerable<string>> ExtractUrlsAsync()
        {
            var htmlPageProcessor = new HtmlPageProcessor();

            while(_storage.GetTraversingUrls().Any())
            {
                // Get the next available URL
                string url = _storage.GetNextTraversingUrl();
                try
                {
                    if(string.IsNullOrWhiteSpace(url))
                    {
                        continue;
                    }

                    // Define url's Loader settings & Parser
                    var loaderSettings = LoaderSettingsProvider.GetLoaderSettings(url);
                    var parser = HtmlPageParserProvider.GetHtmlPageParser(url);

                    // Load the HTML page 
                    htmlPageProcessor.LoaderSettings = loaderSettings;
                    htmlPageProcessor.HtmlPageParser = parser;
                    await htmlPageProcessor.LoadAsync(url);

                    // Parse the HTML page to extract all URLs
                    var extractedUrls = htmlPageProcessor.GetExtractedUrls();
                    _storage.AddUrlsToStorage(extractedUrls);

                    // Get URLs that processor defined as for traversing
                    var traverseUrls = htmlPageProcessor.GetTraversingUrls();
                    _storage.AddUrlsToTraverse(traverseUrls);
                }
                catch (Exception e)
                {
                    Logger.LogException(e);
                }

                // Add the URL as traversed
                _storage.AddTraversedUrl(url);
            }

            return _storage.GetUrls();
        }
    }
}
