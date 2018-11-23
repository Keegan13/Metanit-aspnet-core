using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenMiddleware;
using Microsoft.AspNetCore.Builder;

namespace TokenMiddleware.NestedNameSpace
{
    public static class TokenExtensions
    {
        //not sure 'bout right way on how to call all this several ctors thing
        public static IApplicationBuilder UseToken(this IApplicationBuilder app, string token = null)
        {
            return app.UseMiddleware<TokenMiddleware>(token);
        }

        public static IApplicationBuilder UseToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenMiddleware>();
        }
    }
}
