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
            if (String.IsNullOrWhiteSpace(name))
            {
                return "Hello World";
            }

            var hostName = Dns.GetHostName();
            return "Hello " + name + ", " + hostName +"v2"; 
        }
    }
}