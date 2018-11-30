using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_2._11
{
    public class User
    {
        public int id;
        public string Name;
    }
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options=> {

                options.Cookie.Name = "SessionCookie";
                //options.Cookie.Expiration = new TimeSpan(1,0,0);
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                
                options.Cookie.MaxAge = new TimeSpan(2, 0, 0);
                
                

            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCookiePolicy();

            #region issue
            ////what if cant convert to string
            //context.Items["Name"] = new Tuple<int, int>(5,6);

            //string SharedItem = (string)context.Items["Name"]??null;//Exeption cant cast to string

            ////solution
            //string SharedItem2 = context.Items["Name"] as string;

            //await _next.Invoke();


            #endregion


            #region Code_01
            //app.Use(async (context, _next) =>
            //{
            //    Set middleware

            //   context.Items["Name"] = "Tom";
            //    await _next.Invoke();

            //});
            //app.Run(async context =>
            //{


            //    string SharedItem = context.Items["Name"] as String;
            //    if (!String.IsNullOrEmpty(SharedItem))
            //    {
            //        DoStuff(SharedItem);
            //        await context.Response.WriteAsync($"The name is {SharedItem}");

            //    }
            //    else
            //        await context.Response.WriteAsync("Name not found");



            //});
            #endregion


            #region Code_02
            app.UseSession();
            app.Use(async (context, _next) => {

                if (!context.Session.Keys.Contains("OK"))
                {
                    User u = new User() { id = 15, Name = "Keegan" };
                    context.Session.Set("key", u);
                    context.Session.SetString("OK", "OK");
                }

                
                await _next();

            });
            app.Use(async (context, _next) =>
            {
                string append;
                
                if (context.Session.Keys.Contains("name"))

                    append = $"hi there { context.Session.GetString("name")}";

                else
                {
                    context.Session.SetString("name", "Tom");
                    append = "Name set";
                }
                await _next();
                await context.Response.WriteAsync(append);
            });
            app.Use(async (context, _next) => {

                
                User u=context.Session.Get<User>("key");
                await _next();
                await context.Response.WriteAsync(u.Name+Environment.NewLine);
                await context.Response.WriteAsync(context.Connection.Id+Environment.NewLine);

            });

            //app.Use(async (context, _next) =>
            //{
                   
            //});

            app.UseMvcWithDefaultRoute();
            #endregion

            
            //app.Map("/cookies", _ =>
            //{
            //    _.Run((context) =>
            //    {



            //        //var cookie = context.Request.Cookies["uid"];
            //        //if (cookie!= null)
            //        //    await context.Response.WriteAsync($"Your cookies is {cookie}");
            //        //else
            //        //{
            //        //    context.Response.Cookies.Append("uid", "25", new CookieOptions {
            //        //        Domain = "LocalHost",
            //        //        Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(30, 0, 0, 0)),

            //        //        IsEssential = true;

            //        //    });
            //        //    await context.Response.WriteAsync("Cookies is ready");

            //        //}
            //    });
            //});

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            //p.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

    }
}
