namespace WebParser.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class GlobalStorageInMemory
    {
        private static HashSet<string> _traversedUrls = new HashSet<string>();

        private static HashSet<string> _traversingUrls = new HashSet<string>();

        private static HashSet<string> _urlStorage = new HashSet<string>();


        public GlobalStorageInMemory(string seedUrl)
        {
            if(string.IsNullOrWhiteSpace(seedUrl))
                throw new ArgumentNullException("URL is empty");

            _traversingUrls.Add(seedUrl);
        }

        /// <summary>
        ///     Adds a traversed URL to the Global storage.
        /// </summary>
        /// <param name="url">A traversed URL.</param>
        public void AddTraversedUrl(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("URL is empty");

            _traversedUrls.Add(url);
            _traversingUrls.Remove(url);
        }

        /// <summary>
        ///     Adds an URL to the Global storage. 
        /// </summary>
        /// <param name="url">URL</param>
        public void AddUrlToStorage(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("URL is empty");

            _urlStorage.Add(url);
        }

        public void AddUrlsToStorage(IEnumerable<string> urls)
        {
            if(urls == null)
                throw new ArgumentNullException("URLs not specified");
            
            _urlStorage.UnionWith(urls);
        }

        /// <summary>
        ///     Adds an URL to the Global storage to traverse further
        /// </summary>
        /// <param name="url"></param>
        public void AddUrlToTraverse(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("URL is empty");

            if(_traversingUrls.Count < InitialCapacity)
            {
                _traversingUrls.Add(url);
            }
        }

        public void AddUrlsToTraverse(IEnumerable<string> urls)
        {
            if(urls == null)
                throw new ArgumentNullException("URLs not specified");

            int availableSpace = InitialCapacity - (_traversingUrls.Count + _traversedUrls.Count);
            if(availableSpace > 0)
            {
                if(urls.Count() > availableSpace)
                {
                    urls = urls.Take(availableSpace).ToList();
                }

                urls = urls.Except(_traversedUrls);
                _traversingUrls.UnionWith(urls);
            }
        }

        /// <summary>
        ///     Returns all URLs for traversing
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetTraversingUrls() => _traversingUrls;

        /// <summary>
        ///     Returns all collected URLs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetUrls() => _urlStorage;

        /// <summary>
        ///     Returns the next available URL for traversing.
        /// </summary>
        /// <returns>Url</returns>
        public string GetNextTraversingUrl() => _traversingUrls.Any() 
                                                ? _traversingUrls.First() : string.Empty;

        /// <summary>
        ///     Returns true if an URL is traversed, otherwise false.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>true or false</returns>
        public bool IsUrlTraversed(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("URL is empty");

            return _traversedUrls.Contains(url);
        }

        /// <summary>
        ///     Sets or gets the initial number of max URLs in the Global storage for traversing
        /// </summary>
        /// <value></value>
        public int InitialCapacity { set; get; }
    }
}