using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace System
{
    public class HomeController:Controller
{

        public HomeController()
        {
            Random R = new Random();
            this.value = R.Next(0, 100);
        }
        public bool Exists;
        public int value=0;
        public string Index()
        {
            return this.value.ToString()+" "+HttpContext.Session.Id;
        }

}
}
