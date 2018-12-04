using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_3._3.Models;
using col = System.Collections;

namespace AspNetCore_3._3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Phones([FromServices] StoreContext context)
        {
            
            if(context!=null)
            return View(context.Phones.ToList());

            
            

            return new NotFoundResult();
        }
        [HttpGet]
        public IActionResult Buy([FromServices] StoreContext context, int? id)
        {
            if (id.HasValue && context.Phones.FirstOrDefault(x => x.Id == id) is Phone phone)
            {
                
                return View(new Order { Phone=phone, PhoneId=id.Value});
            }

            return RedirectToAction("Phones");
        }

        [HttpPost]
        public string Buy([FromServices] StoreContext context, Order order)
        {
            var data=Stringify(HttpContext.Request.Form.Select(x=>KeyValuePair.Create<string,object>(x.Key,x.Value)));
            int phoneid;
            if (!int.TryParse(HttpContext.Request.Form["PhoneId"].ToString(), out phoneid))
                return "Phone id not provided";

            Phone phone= context.Phones.FirstOrDefault(x => x.Id == phoneid);
            if (phone == null)
                return "Phone not found";

            order.Phone = phone;
            order.PhoneId = phoneid;
            order.Id = 0;

            

            if (IsValid(order))
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return data;
            }
            else
            return "NOTOK\n\r"+data;
        }

        private static bool IsValid(Order order)
        {
            if (order.Phone == null)
                return false;
            if (order.Address== null)
                return false;
            if (order.User == null)
                return false;
            return true;
        }

        private static string Stringify(IEnumerable<KeyValuePair<string,object>> array)
        {
            string output = "";
            foreach (var item in array)
                output += item.Key + ":\t" + item.Value.ToString() + Environment.NewLine;

            return output;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return About();// View();
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
