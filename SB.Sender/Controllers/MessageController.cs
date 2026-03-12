using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.Sender.Services;
using SB.Shared;
using System.Text.Json;

namespace SB.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly string connectionString = "YOUR_SERVICE_BUS_CONNECTION_STRING";
        private readonly string queueName = "studentqueue";


        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(Student student)
        {
            string messageBody = JsonSerializer.Serialize(student);

            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(queueName);

            ServiceBusMessage message = new ServiceBusMessage(messageBody);

            await sender.SendMessageAsync(message);

            return Ok(new { Status = "Student message sent successfully!" });
        }
    }
}
