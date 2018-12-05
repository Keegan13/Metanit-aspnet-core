using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IActionResultImplementation
{


    public class MyActionResultImplementatation : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.HttpContext.Response.WriteAsync("This is IActionResult"+Environment.NewLine+IActionResultImplementation.Extensions.IActionResultExtensions.PrintAll(context.RouteData));
        }

        
    }

    namespace Extensions
    {
        public static class IActionResultExtensions
        {
            public static string PrintAll(object context)
            {
                return JsonConvert.SerializeObject(context,settings:new JsonSerializerSettings { ReferenceLoopHandling= ReferenceLoopHandling.Ignore,Formatting=Formatting.Indented });
            }
        }
    }
}
