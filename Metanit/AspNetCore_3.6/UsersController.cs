using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore_3._6.Controllers
{
    public class UsersController : Controller
    {

        public IActionResult Index()
        {
            string name = HttpContext.GetRouteValue("username")?.ToString()??"none"; 


            return View(new User { Id=0,Name=name});
        }

    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}