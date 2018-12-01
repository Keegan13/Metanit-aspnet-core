using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCore_11._2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Main
            //store routes
            var AvaliableRoutes = new List<Tuple<string, string>>();

            var builder = new RouteBuilder(app);

            foreach (var item in new[] { "bool","int", "alpha","minlength(3)" ,"regex(^H.*)"})
            {
                // setting route
                // suppose item="int"
                // then route="int/{value:int}"
                // means that URL like https://localhost:xxxx/int/4343 - will work
                // where value=4343, now you can vary value and watch how 'item' constraint 
                // affects URL matching

                string route = item + "/{value:" + item + "}";
                // store avaliable routes somewhere to display them URL mathing failed
                AvaliableRoutes.Add(new Tuple<string, string>(item, route));
                // add routes with their handlers to RouteBuilder
                builder.MapGet(route, async (context) =>
                {
                    //get velue from RouteData
                    string value = context.GetRouteValue("value").ToString();
                    //get request path
                    string path = context.Request.Path.ToString();
                    //make response message
                    string message = $"Request Path={path}"+Environment.NewLine+$"value={value}";
                    //set content type to plain text istead of html, why? who knows
                    context.Response.ContentType = "text/plain";
                    //write message to output stream
                    await context.Response.WriteAsync(message);
                });

            }

            app.UseRouter(builder.Build());
            

            app.Run(async (context) =>
            {
                //set left part of URI
                string host = (context.Request.IsHttps ? "https://" : "http://") + context.Request.Host.Value;

                string output = "<ul>";

                //add href tag
                foreach (var item in AvaliableRoutes)
                    output += $"<li><a href={host}/{item.Item1}/><b>{item.Item2}</b></a></li>";
                output += "</ul>";
                context.Response.ContentType = "text/html charset=UTF-8";
                await context.Response.WriteAsync(output);

            });
            #endregion
        }
    }
}
