using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.IO;

namespace Microsoft.Extensions.Configuration
{
    public class CustomConfigurationProvider : ConfigurationProvider
    {
       public string FilePath { get; set; }
        public CustomConfigurationProvider(string path)
        {
            this.FilePath = path;
        }
        public override void Load()
        {
            IDictionary<string, string> LoadedData = new Dictionary<string, string>();

            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    string line;
                    while ((line=sw.ReadLine())!=null)
                    {
                        string key = line.Trim();
                        string value = sw.ReadLine();
                        if (LoadedData.ContainsKey(key))
                        {
                            LoadedData[key] = value;
                        }
                        else
                        {
                            LoadedData.Add(key, value);
                        }
                        
                    }
                }
            }
            this.Data = LoadedData;
        }
    }

    public class CustomConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; private set; }
        public CustomConfigurationSource(string FilePath)
        {
            this.FilePath = FilePath;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {

            string filepath = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            
                return new CustomConfigurationProvider(filepath);
        
        }
    }

    public static class CustomConfigProviderExtension
    {
        public static IConfigurationBuilder AddCustom(this IConfigurationBuilder builder,string path)
        {

            if (builder == null || path == null)
                throw new NullReferenceException("One of the parametrs is not set {null}");

            builder.Add(new CustomConfigurationSource(path));
            return builder;
        }
    }
}
