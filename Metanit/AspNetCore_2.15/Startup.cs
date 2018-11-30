using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace AspNetCore_2._15
{
    public class Startup
    {
        IConfiguration AppConfiguratioin;


        public Startup(IConfiguration config)
        {
            this.AppConfiguratioin = config;

            var builder = new ConfigurationBuilder();
            //builder.AddConfiguration(config);
            builder.SetBasePath(System.IO.Directory.GetCurrentDirectory());

            builder.AddCustom("config.txt");
            this.AppConfiguratioin = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Replace(new ServiceDescriptor(typeof(IConfiguration),this.AppConfiguratioin));
            services.AddTransient(typeof(AnyClass));
            //var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory());
            //builder.AddCustom("config.txt");

            //this.AppConfiguratioin = builder.Build();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration cnf)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {

               await context.Response.WriteAsync(Stringify(cnf));
               // await context.Response.WriteAsync(cls.GetConnectionString());
            });
        }
        public static string Stringify(IConfiguration cnf)
        {
            string output="";
            if (cnf!=null)
            foreach (var item in cnf.AsEnumerable())
                output += item.Key + "\t" + item.Value+Environment.NewLine;
            else
                output = "No configuration given";
            return output;
        }
    }
}
