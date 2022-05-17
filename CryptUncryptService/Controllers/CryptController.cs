using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptUncryptService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptController : ControllerBase
    {
        private ICheckToken _checkToken;

        ConnectionFactory factory;
        public CryptController(ICheckToken checkToken)
        {
            _checkToken = checkToken;

            factory = new ConnectionFactory();
            factory.Uri = new Uri("xxx");
        }

        [HttpPost("GetEncryptData")]
        public IActionResult GetEncryptData(string data, string token)
        {
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("istekkuyrugu", false, false, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume("istekkuyrugu", false, consumer);
                string aa;
                consumer.Received += (sender, e) =>
                {
                    aa = Encoding.UTF8.GetString(e.Body.ToArray());
                };

            }
            if (_checkToken.ValidateToken(token))
                return Ok(Tools.Encrypt(data, "pass1*_").ToString());

            return Unauthorized();
        }

        [HttpPost("GetDecryptData")]
        public IActionResult GetDecryptData(string data, string token)
        {
            if (_checkToken.ValidateToken(token))
            {
                return Ok(Tools.Decrypt(data, "pass1*_").ToString());
            }
            return Unauthorized();

        }




    }
}
