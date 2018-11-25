using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_2._8
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
            
            app.UseOwin((action)=> {

                action(Middleware1);
                action(Middleware1);


            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
        //1 param in UseOwin
        public void PipeLineExp(Action<Func<Func<IDictionary<string,object>,Task>, Func<IDictionary<string, object>, Task>>> middleware)
        {
            middleware(Middleware1);
        }
        public Func<IDictionary<string, object>, Task> Middleware1(Func<IDictionary<string, object>,Task> Next)
        {


            return Next;
        }

        

        public Task DoStuff(IDictionary<string, object> environment)
        {
            // определяем ответ
            string responseText = "Hello ASP.NET Core";
            // кодируем его в массив байтов
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            // получаем поток ответа
            var responseStream = (Stream)environment["owin.ResponseBody"];
            // отправка ответа
            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);

        }
    }
}
