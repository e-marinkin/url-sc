namespace WebParser.Core
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IHtmlParser
    {
        /// <summary>
        ///     Performs
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> ExtractUrlsAsync();
    }
}