namespace WebParser.Core
{
    using System;

    public class Logger
    {
        public static void LogException(Exception e)
        {
            if(e != null)
                Console.WriteLine($"Exception:{e.Message}");
        }
    }
}