using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_11._3
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
            var builder = new RouteBuilder(app);
            builder.DefaultHandler = new RouteHandler(async context => await context.Response.WriteAsync("You've entered rigth route"));

            builder.MapRoute(
                name: "default",
                template: "{area}/{username}/{controller}/{action}",
                constraints: new {
                    username = new RequiredRouteConstraint(),
                    area = new MaxLengthRouteConstraint(5)
                },
                defaults: new {
                    controller = "Home",
                    action = "index"
                });
            
            

            app.UseRouter(builder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
