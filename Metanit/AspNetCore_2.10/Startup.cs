using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Custom;

namespace AspNetCore_2._10
{
   
    public class Startup
    {
        //readonly ILogger _name;
        //readonly ILoggerFactory _logfact;

        //public Startup(ILoggerFactory factory)
        //{
        //    //this._name = logger;
        //    this._logfact = factory;
        //}
            
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // this._logfact.AddConsole(LogLevel.Trace);
            var _logfact = app.ApplicationServices.GetService<ILoggerFactory>();
            var loggefr = app.ApplicationServices.GetService<ILogger>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _logfact.AddFile(Path.Combine(Directory.GetCurrentDirectory(),"Log.txt"));

            var logger = _logfact.CreateLogger("FileLogger");
            _logfact.AddConsole(LogLevel.Debug);
            app.Run(async (context) =>
            {
                logger.LogInformation("{0} Processing request {1} from {2}",DateTime.Now.ToString(),context.Request.Path,context.Request.Host.Value.ToString());
                //var logger=this._logfact.CreateLogger("RequestInfoLogger");
                //logger.LogTrace("Message");
                //logger.LogDebug("Debug message");
                //logger.LogError("Error message");
                //logger.LogInformation("Processing request {0}", context.Request.Path);

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
