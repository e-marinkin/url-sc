namespace WebParser.Core
{
    public class DefaultLoaderSettings : ILoaderSettings
    {
        public bool IsAvailableToLoad(string url)
        {
            return true;
        }

        public bool IsAvailableToParse(string html)
        {
            return true;
        }
    }
}