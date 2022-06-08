using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Album.Api
{
    public class GreetingService
    {
        public static string Greeting(string name)
        {
            // loggingservice.info("greeting service is called with name " + name);

            if (String.IsNullOrWhiteSpace(name))
            {
                return "Hello World";
            }

            var hostName = Dns.GetHostName();
            return "Hello " + name + ", " + hostName; 
        }
    }
}