using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;


namespace MyMiddleware
{
    public static partial class Ext
    {
        public static IApplicationBuilder UseRoutingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RoutingMiddleware>();
        }
    }
    public class RoutingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RoutingMiddleware(RequestDelegate NextMiddleware)
        {
            this._next = NextMiddleware;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();
            if (path == "/index")
            {
                await context.Response.WriteAsync("This is index page");
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("This is about page");
            }
            else
            {
                context.Response.StatusCode = 403;
            }

            //await this._next.Invoke(context);
        }
    }
    
}
