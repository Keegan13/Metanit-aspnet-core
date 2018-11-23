using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMiddleware
{
    public static partial class Ext
    {
        public static IApplicationBuilder UseErrorss(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
    public class ErrorHandlingMiddleware
    {
        readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate Next)
        {
            this._next = Next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);

            var status = context.Response.StatusCode;
                        
            if (status == 403)
            {
                await context.Response.WriteAsync("You have no access to this page, please provide token to proff you identity");
            }
            else if(status==404)
            {
                await context.Response.WriteAsync("Page not found");
            }
            
        }
    }
}
