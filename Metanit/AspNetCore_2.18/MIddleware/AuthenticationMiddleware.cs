using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MyMiddleware
{
    public static partial class Ext
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
    public class AuthenticationMiddleware
    {

        readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate NextMiddleware)
        {
            this._next = NextMiddleware;
        }


        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await this._next.Invoke(context);
            }


            
        }

    }
}
