using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace System
{
    public class AnyClass
{
        IConfiguration AppConfiguration;
        public AnyClass(IConfiguration config)
        {
            this.AppConfiguration = config;
        }

        public string GetConnectionString()
        {
            var section=AppConfiguration.GetSection("ConnectionStrings");
            return section["TestConnection"];
            
        }
}
}
