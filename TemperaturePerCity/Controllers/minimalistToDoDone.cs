using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JSONController : ControllerBase
    {
        private readonly ILogger<JSONController> _logger;
        private readonly List<JSONObject> _objects = new List<JSONObject>();
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _serviceBusSender;

        public JSONController(ILogger<JSONController> logger, ServiceBusClient serviceBusClient)
        {
            _logger = logger;
            _serviceBusClient = serviceBusClient;
            _serviceBusSender = _serviceBusClient.CreateSender("my-queue");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JSONObject obj)
        {
            _objects.Add(obj);

            if (obj.messages != null && obj.messages.Count > 0)
            {
                foreach (var message in obj.messages)
                {
                    var serviceBusMessage = new ServiceBusMessage(message);
                    await _serviceBusSender.SendMessageAsync(serviceBusMessage);
                }
            }

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
