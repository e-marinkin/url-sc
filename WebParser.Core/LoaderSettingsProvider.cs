namespace WebParser.Core
{

    using System;
    using System.Collections.Generic;

    public class LoaderSettingsProvider
    {
        private static readonly Dictionary<string,Type> _loaders = new Dictionary<string, Type>{
            {"https://www.flowerstation.co.uk", typeof(FlowersLoaderSettings)}                
        };

        public static ILoaderSettings GetLoaderSettings(string basedUrl)
        {
            if(string.IsNullOrWhiteSpace(basedUrl))
                throw new ArgumentNullException("URL is empty");

            // If the passed url the same
            if(_loaders.ContainsKey(basedUrl))
            {
                return (ILoaderSettings)Activator.CreateInstance(_loaders[basedUrl]);
            } 
            // Try to find by its domain
            else 
            {
                var uri = new Uri(basedUrl);
                string domain = uri.GetLeftPart(UriPartial.Authority);
                if(!string.IsNullOrWhiteSpace(domain) && _loaders.ContainsKey(domain))
                {
                    return (ILoaderSettings)Activator.CreateInstance(_loaders[domain]);
                }
            }

            return new DefaultLoaderSettings();
        }
    }
}