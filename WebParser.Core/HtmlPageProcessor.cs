namespace WebParser.Core
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using HtmlAgilityPack;

    public class HtmlPageProcessor
    {
        private ILoaderSettings _loaderSettings;
        private IHtmlPageParser _htmlPageParser;

        private IEnumerable<string> _allUrls;
        private IEnumerable<string> _traversingUrls;

        public ILoaderSettings LoaderSettings{
            get { 
                if(_loaderSettings == null)
                {
                    _loaderSettings = new DefaultLoaderSettings();
                }
                return _loaderSettings;
            }
            set => _loaderSettings = value;
        }

        public IHtmlPageParser HtmlPageParser {
            get {
                if(_htmlPageParser == null)
                {
                    _htmlPageParser = new DefaultHtmlPageParser();
                }
                return _htmlPageParser;
            }
            set => _htmlPageParser = value;
        }

        public async Task LoadAsync(string url)
        {
            string html = string.Empty;

            // Check if it's possible to pre-load web path
            if(!LoaderSettings.IsAvailableToLoad(url))
            {
                return;
            }

            using(var client = new HttpClient())
            {
                try
                {
                    html = await client.GetStringAsync(url);
                }
                catch (HttpRequestException)
                {
                    throw;
                }
            }

            // Check if it's possible to parse
            if(!LoaderSettings.IsAvailableToParse(html))
            {
                return;                    
            }

            try
            {
                 HtmlPageParser.Parse(html);

                _allUrls = HtmlPageParser.GetAllUrls();
                _traversingUrls = HtmlPageParser.GetTraversingUrls();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<string> GetExtractedUrls() => _allUrls ?? Array.Empty<string>();

        public IEnumerable<string> GetTraversingUrls() => _traversingUrls ?? Array.Empty<string>();
        
    }
}