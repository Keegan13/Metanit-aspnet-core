using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_19._1.Controllers
{
    public class Account
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Account LoopIn { get; set; }

    }

    public class TestController:Controller
    {
        public IActionResult Get()
        {
            return new RedirectResult("~/Account/getjson");
        }
    }

    public class AccountController : Controller
    {
        public IActionResult getjson()
        {

            //Account a = new Account()
            //{
            //    Name = "Luke",
            //    Password = "1234",
            //    Email = "very@mail.kz"
            //};

            //Account b = new Account()
            //{
            //    Name = "Darte",
            //    Password = "1234",
            //    Email = "very2@mail.kz",
            //    LoopIn = a
            //};
            //a.LoopIn = b;
            var result = JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                MaxDepth=2,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects


            });

            return Content(result);

            //return Json(this, new Newtonsoft.Json.JsonSerializerSettings
            //{
            //    Formatting = Newtonsoft.Json.Formatting.Indented
            //});

            //return Json(this.ControllerContext, new Newtonsoft.Json.JsonSerializerSettings
            //{
            //    Formatting = Newtonsoft.Json.Formatting.Indented
            //});
        }

        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(Account acc)
        {
            if (ModelState.IsValid)
            {
                return Content($"{acc.Name}-{acc.Password}-{acc.Email}");
            }
            else
                return View(acc);
        }
    }
}
