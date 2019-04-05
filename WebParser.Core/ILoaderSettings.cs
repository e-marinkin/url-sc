namespace WebParser.Core
{
    /// <summary>
    ///     Provides a behavior for loading Html pages.
    /// </summary>
    public interface ILoaderSettings
    {

        /// <summary>
        ///     Performs validations before a Html page will be uploaded via url. 
        /// If it is available to load then returns true, otherwise false.
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>true or false</returns>
        bool IsAvailableToLoad(string url);

        /// <summary>
        ///     Performs validations after a Html page is uploaded via url. 
        /// If it is available to process the Html page then returns true, otherwise false.
        /// </summary>
        /// <param name="html">Html string to validate</param>
        /// <returns>true or false</returns>
        bool IsAvailableToParse(string html);
    }
}