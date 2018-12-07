using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_7._4.Models;
using AspNetCore_7._4;

namespace AspNetCore_7._4.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public string Equal([FromServices] IServiceProvider services)
        {
            string output = "";

            output+=Equal(HttpContext.Request,Request);
            output += Equal(HttpContext.RequestServices, services);




            return output;
        }

        private static string Equal(object obj1, object obj2)
        {
            string output = "Objects of type:" + obj1.GetType().ToString() + ":" + nameof(obj1) + " and " + obj2.GetType().ToString() + ":" + nameof(obj2) + " is reference ";
            if (Object.ReferenceEquals(obj1, obj2))
                output += "equal";
            else
                output += "non equal";
            output += Environment.NewLine;
            return output;
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult GetMyClassList()
        {
            return View(new[] { new MyClass(), new MyClass(), new MyClass(), new MyClass(), new MyClass(), new MyClass(), });
        }

        public PartialViewResult GetMyClassView(MyClass item)
        {
            return PartialView("MyClassView", item);
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
