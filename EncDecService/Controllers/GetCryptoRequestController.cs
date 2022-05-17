using EncDecService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EncDecService.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetCryptoRequestController : ControllerBase
    {
        ConnectionFactory factory;
        public GetCryptoRequestController()
        {
            factory = new ConnectionFactory();
            factory.Uri = new Uri("xxx");
        }

        [HttpGet("GetCryptoRequest")]
        public IActionResult GetCryptoRequest(string token, string data)
        {
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("istekkuyrugu", false, false, false);
                byte[] bytemessage = Encoding.UTF8.GetBytes(data);
                channel.BasicPublish(exchange: "", routingKey: "istekkuyrugu", body: bytemessage);
            }
            return Ok();
        }



    }
}
