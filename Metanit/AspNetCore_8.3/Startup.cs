using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetCore_8._3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseWhen(x => x.Request.Path.StartsWithSegments("/getconfig"), App =>
            {

                App.Run(async context =>
                {
                    var config = App.ApplicationServices.GetService<IConfiguration>();
                    var stringCnf = JsonConvert.SerializeObject(config, Formatting.Indented);
                    await context.Response.WriteAsync(stringCnf??"no config");

                });
            });
                

            app.Use(next =>
            {
                return async context =>
                {
                    var config = app.ApplicationServices.GetService<IConfiguration>();
                    if (config != null)
                    {
                        var connString = config.GetConnectionString("DefaultConnection")?.FirstOrDefault().ToString()??"no connection string";
                        await context.Response.WriteAsync(connString);
                    }
                    else
                        await next.Invoke(context);
                };

            });
            app.UseStaticFiles().
                UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{action=Index}/{Name?}/{Email?}",
                    defaults: new { controller = "Home" });
            });

        }







    }
}
