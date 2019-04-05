namespace WebParser.App
{
    using System;
    using System.Threading.Tasks;
    using WebParser.Core;
    class Program
    {
        static void Main(string[] args)
        {
            HtmlParser parser = new HtmlParser("https://www.flowerstation.co.uk/bouquet-of-the-week/");
            var urls = parser.ExtractUrlsAsync().Result;

            foreach (var url in urls)
            {
                Console.WriteLine(url);
            }

            Console.ReadLine();
        }
    }
}
