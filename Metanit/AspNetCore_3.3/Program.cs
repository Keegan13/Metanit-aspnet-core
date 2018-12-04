using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AspNetCore_3._3.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_3._3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope= host.Services.CreateScope())
            {

                StoreContext context = scope.ServiceProvider?.GetService(typeof(StoreContext)) as StoreContext;

                if (context != null)
                {
                    try
                    {
                        context.InitializeDb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    
                }
                
            }


                host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
