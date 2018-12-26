using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_8._3.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace AspNetCore_8._3.Controllers
{
    public class A
    {
        //binding by Name
        public string Name { get; set; }
        //binding by Name
        public string Email { get; set; }
        //binding by property path syntax e.g. "A.B.Name":"Tod"
        public B B { get; set; }

        //collection binding
        public IEnumerable<int> col { get; set; }
    }

    public class B : A
    {

    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult GetA(A a)
        {
            return View();
        }

        public IActionResult GetAPost(A a)
        {
       

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,

            };
            var result = "OBJECT" + Environment.NewLine + JsonConvert.SerializeObject(a, settings) + Environment.NewLine;
             result += "QUERY:" + Environment.NewLine + JsonConvert.SerializeObject(Request.Query, settings) + Environment.NewLine;

            if(Request.HasFormContentType)
                result += "FORM" + Environment.NewLine + JsonConvert.SerializeObject(Request.Form, settings) + Environment.NewLine;
            

            result += "ROUTE" + Environment.NewLine + JsonConvert.SerializeObject(RouteData, settings) + Environment.NewLine;
            



            return Content(result);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
