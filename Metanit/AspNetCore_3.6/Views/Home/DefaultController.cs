using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNamespace
{
    public class DefaultController
    {
        public  string Index()
        {
            double aprox = 0.1;
            return aprox.ToString("F17");
        }
    }
}