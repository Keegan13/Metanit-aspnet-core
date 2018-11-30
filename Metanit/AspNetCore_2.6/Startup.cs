using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Extensions.Options;

namespace AspNetCore_2._6
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }
        public class Source
        {
            public string SProp1 { get; set; }
            public string SProp2 { get; set; }

        }

        public Startup()
        {
            var builder = new ConfigurationBuilder();

            AddMemoryConfig(builder);
            AddJsonConfig(builder);
            AddXmlConfig(builder);
            AddIniConfig(builder);
            //builder.AddEnvironmentVariables();
  
          
            AppConfiguration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Source>(AppConfiguration.GetSection("Source"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //Source s=AppConfiguration.GetValue<Source>("Source");
                //var cnf = AppConfiguration[]; GetSection("json");
                //await context.Response.WriteAsync(Stringify(AppConfiguration));
                //await context.Response.WriteAsync(Stringify(cnf));
                //await context.Response.WriteAsync(AppConfiguration["JsonOnlyField"]);
                Source src = (context.RequestServices.GetService(typeof(IOptionsSnapshot<Source>)) as IOptionsSnapshot<Source>)?.Value;
                if (src != null)
                    await context.Response.WriteAsync(src.SProp1 + src.SProp2);
                else
                    await context.Response.WriteAsync("cant find Source service");


            });
        }
        public static void AddMemoryConfig(IConfigurationBuilder builder)
        {
            IDictionary<string, string> KeyValueConfig = new Dictionary<string, string> {
                {"Source","Memory"},
                {"InMemoryOnlyField","InMemoryOnlyField_Value" },
                {"SourceFake","SourceFake_Value" }

            };
            var cnfstr = JsonConvert.SerializeObject(KeyValueConfig);
            builder.AddInMemoryCollection(KeyValueConfig); 
        }
        public  static void AddJsonConfig(IConfigurationBuilder builder)
        {
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "config.json");
            string pathComplex = Directory.GetCurrentDirectory()+"\\config-l.json";
            if (!System.IO.File.Exists(path))
                File.Create(path);
            IDictionary<string, string> KeyValueConfig = new Dictionary<string, string> {
                {"Source","Json"},
                {"JsonOnlyField","JsonOnlyField_Value" }
                };
            var cnfstr = JsonConvert.SerializeObject(KeyValueConfig);
             File.WriteAllText(path, cnfstr);

        builder
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path,false,true)//it is possible to add several files
                .AddJsonFile(pathComplex,false,true);           
        }
        public static void AddXmlConfig(IConfigurationBuilder builder)
        {
            string path = Directory.GetCurrentDirectory()+"\\config.xml";
            
            //if (!System.IO.File.Exists(path))
            //    File.Create(path);
            //IDictionary<string, string> KeyValueConfig = new Dictionary<string, string> {
            //    {"Source","XML"},
            //    {"XmlOnlyField","XmlOnlyField_Value" }
            //    };
           

            //XmlSerializer xml = new XmlSerializer(typeof(KeyValuePair<string,string>));

            //string s = "";


            //using (var sww = new StringWriter())
            //{
            //    using (XmlWriter writer = XmlWriter.Create(sww,new XmlWriterSettings { ConformanceLevel=ConformanceLevel.Fragment}))
            //    {
                    
            //      //  foreach (KeyValuePair<string, string> item in KeyValueConfig.AsEnumerable())

            //            xml.Serialize(writer, item);

                    
            //        //File.WriteAllText(path,sww.ToString());
            //    }
            //}


            
            builder.AddXmlFile(path);
            
        }
        public static void AddIniConfig(IConfigurationBuilder builder)
        {

            var path = Directory.GetCurrentDirectory() + "\\config.ini";
            builder.AddIniFile(path);
        }


        public static string Stringify(IConfiguration cnf)
        {
            string StrConfig = "";
            foreach (KeyValuePair<string, string> item in cnf.AsEnumerable())
                StrConfig+=item.Key+"\t"+item.Value+Environment.NewLine;
            return StrConfig;
        }

        public static string Stringify(IEnumerable<IConfigurationSection> sections)
        {
            string StrConfig = "";
            foreach (IConfigurationSection section in sections)
            {
                StrConfig += section.Key+Environment.NewLine;

                foreach (KeyValuePair<string, string> item in section.AsEnumerable())
                    StrConfig += item.Key + "\t" + item.Value + Environment.NewLine;
            }
             
                return StrConfig;
        }


    }
}
