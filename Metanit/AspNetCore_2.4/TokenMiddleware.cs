using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TokenMiddleware
{
    namespace NestedNameSpace
    {
        public class TokenMiddleware
        {
            int _requestsCount = 0;
            private readonly RequestDelegate _next;
            private readonly string _token;
            public TokenMiddleware(RequestDelegate NextMiddleware) : this(NextMiddleware, null)
            {

            }
            public TokenMiddleware(RequestDelegate NextMiddleware, string token = null)
            {
                if (token != null)
                {
                    this._token = token;
                }
                this._next = NextMiddleware;
            }


            public async Task InvokeAsync(HttpContext context)
            {
                var token = context.Request.Query["token"];
                this._requestsCount++;

                if (this._token != null && token != this._token)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync($"Token is invalid: </br> This middleware was called {this._requestsCount} times");
                }
                else
                {
                    await _next.Invoke(context);
                }

            }

        }
    }
    
}
