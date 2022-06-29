using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Api;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace Album.Api.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("/api/hello/{name}")]
        [SwaggerOperation(Summary = "This is a summary", Description = "desc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Hello(string name)
        {
            return GreetingService.Greeting(name);
        }
    }
}