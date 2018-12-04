
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_3._6.Controllers
{
    public class HomeController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
