using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_2._22
{
    //internal , avalible from this assambly 
    static class UseExtensions
    {
        //public extension method, 
        public static void AddWriteUrl(this IApplicationBuilder app)
        {
            //middleware that adds to response BIG H1 tage with requested url
            app.Use(async (context, next) =>
            {
                
                var request = context.Request;
                //add txt to begining of html request,
                await context.Response.WriteAsync($"<h1 align=\"center\">{request.Scheme}://{request.Host.Value}{request.Path.Value}</h1>");
                //invoke next middleware
                await next.Invoke();
            });

        }
      

    }

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

            //use first route that matches 
            

            app.AddWriteUrl();
            
            app.Map("/1/2/3/index", _ => {

                _.Run(async context => await context.Response.WriteAsync("this is from full map"));

            });
            ///1/2/3/index will hide this
            ///_____S____P____A____G____H____E____T____T____I____
            app.Map("/1", _ => {
                _.Map("/2/3/index",x=> {
                    x.Run(async context=> await context.Response.WriteAsync("This from nasted map"));
                });
                _.Run(async context => await context.Response.WriteAsync("this is from wrong nasted map sdhfsdhfsdhfk"));
            }
                );

            app.Map("/index.php",_=>{

                _.Run(async context => await context.Response.WriteAsync("There is no php here ist asp.net core app"));

            });
            
            app.MapWhen((context)=>{
                return context.Request.Path.Value=="/222";
            },_=>_.Run(async http=>await http.Response.WriteAsync("this is from map when, HI!")));
            
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
        

    }
}
