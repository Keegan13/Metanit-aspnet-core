using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore_5._4.Controllers
{
    //wtf
    public abstract class AbstractController : Controller
    {
        public virtual int ControllerId { get; set; }
        
        public override string ToString()
        {
                

            return base.ToString();
        }
        public virtual IActionResult GetHeaders([FromServices] int sum)
        {
            string output = "";
            foreach (var item in this.Request.Headers)
            {
                output += $"<tr><td>{item.Key}</td><td>{item.Value}</td></tr>";
            }
            


            return Content(content:$"<table>{output}</table>",contentType:"text/html");
        }

        //public virtual IActionResult GetServices()
        //{
        //    //string output = "";
        //    //var provider= HttpContext.RequestServices


        //    //foreach (var item in )
        //    //{ }

        //    //return Content(,contentType:"text/html");
        //}

    }
}