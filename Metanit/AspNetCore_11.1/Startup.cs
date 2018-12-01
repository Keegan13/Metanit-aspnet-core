using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_11._1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Display all components and their values
            RouteHandler handler= new RouteHandler(async context => {
                await context.Response.WriteAsync(Stringify(context.GetRouteData()
                    .Values
                    .Select(x => KeyValuePair.Create<string, string>(x.Key, (string)x.Value))));
            });

            //Map handler to URI like .../Id/A13Andrew
            app.UseRouter(new RouteBuilder(app, handler).MapRoute("getall", "Id/{username}/{Area=Profile}/{Controller?}/{Actionn?}/{*all}").Build());


            //Display controller and action components values 
            handler = new RouteHandler(async (context)=> {
                
                context.Response.ContentType = "text/html charset=UTF-8";
                string Controller = context.GetRouteData().Values["Controller"].ToString();
                string Action = context.GetRouteValue("Action").ToString();
                string other = context.Request.Query.Select(x => x.Key +"="+ x.Value.ToString()).Aggregate((x1,x2)=>x1+="</br>"+x2);
                await context.Response.WriteAsync($"<p>You've entered custom path where:</p> </br>Controller={Controller}</br>Action={Action}</br>Parametrs:{other}");
            });

            IRouteBuilder builder = new RouteBuilder(app,handler);
            builder.MapRoute("default", "{Controller}/{Action}");
            app.UseRouter(builder.Build());



            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("No url match");
            });
        }
        public static string Stringify(IEnumerable<KeyValuePair<string,string>> array)
        {
            string output = "";
            foreach (KeyValuePair<string,string> item in array)
                output += item.Key+"="+item.Value+Environment.NewLine;

            return output;
        }
    }
}
