using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JSONController : ControllerBase
    {
        private readonly ILogger<JSONController> _logger;
        private readonly List<JSONObject> _objects = new List<JSONObject>();

        public JSONController(ILogger<JSONController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] JSONObject obj)
        {
            _objects.Add(obj);
            // TODO: send optional messages to Azure Service Bus
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_objects);
        }
    }

    public class JSONObject
    {
        public List<string> data { get; set; }
        public List<string> messages { get; set; }
    }
}
