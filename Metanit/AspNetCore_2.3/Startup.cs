using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_2._3
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, nextMW) =>
            {

                await nextMW.Invoke();

            });

            //int x = 2;
            //int y = 1;
            //int z = 0;

            //app.Use(async (context, next) =>
            //{
            //    y *= x;
            //    await next.Invoke();

            //});
            ////app.Run
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"y={y}");
            //});

            #region bad practice
            //app.Use(async (context, next) =>
            //{

            //    await context.Response.WriteAsync("<p>Hello world!</p>");
            //    await next.Invoke();
            //});

            //app.Run(async (context) =>
            //{
            //    //await Task.Delay(10000); //можно поставить задержку
            //    await context.Response.WriteAsync("<p>Good bye, World...</p>");
            //});

            #endregion

            #region invoke chain

            int x = 2;
            app.Use(async (context, next) =>
            {
                x = x * 2;      // 2 * 2 = 4
                await next.Invoke();    // вызов app.Run
                x = x * 2;      // 8 * 2 = 16
                await context.Response.WriteAsync($"Result: {x}");
            });

            app.Run(async (context) =>
            {
                x = x * 2;  //  4 * 2 = 8
                await Task.FromResult(0);
            });

            #endregion


        }
    }
}
