using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Api;

namespace Album.Api.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("/api/hello/{name}")]
        public string Hello(string name)
        {
            return GreetingService.Greeting(name);
        }
    }
}