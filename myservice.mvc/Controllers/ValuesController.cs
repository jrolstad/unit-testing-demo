using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public OkObjectResult Get()
        {
            var data = new string[] { "value1", "value2" };

            return Ok(data);
        }

        [HttpGet("{id}")]
        public OkObjectResult Get(int id)
        {
            return Ok("value");
        }
    }
}
