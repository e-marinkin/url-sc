namespace WebParser.Core
{
    using System;
    public class FlowersLoaderSettings : ILoaderSettings
    {
        public bool IsAvailableToLoad(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
                return false;

            var uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Authority) == "https://www.flowerstation.co.uk";
        }

        public bool IsAvailableToParse(string html)
        {
            return true;
        }
    }
}