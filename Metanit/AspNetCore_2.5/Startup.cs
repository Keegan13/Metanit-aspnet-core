using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace AspNetCore_2._5
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //DefaultFilesOptions df = new DefaultFilesOptions();
            //df.DefaultFileNames.Add("phpinfo.html");
            //app.UseDefaultFiles(df);
            //app.UseStaticFiles();
            //app.UseDirectoryBrowser();
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions() {
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"static")),
            //    RequestPath = new PathString("/ftp/user/call/out")
            //});
            app.UseFileServer(true);

            
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Hello World!{df.DefaultFileNames.Aggregate((str1,str2)=>str1+"; "+str2)}");
            //});
        }
    }

}
