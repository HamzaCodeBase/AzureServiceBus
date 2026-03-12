using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SB.Shared;
using System.Text.Json;

namespace SB.Sender.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _config;

        public MessageService()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _config = config;
        }
        public async Task<bool> SendMessageAsync(Student student, string QueueName)
        {
            String body = JsonSerializer.Serialize(student);
            ServiceBusMessage message = new ServiceBusMessage(body);

            var connectionString = _config.GetConnectionString("MyConnection");
            var queueName = _config.GetSection("QueueName");

            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusSender sender= client.CreateSender(QueueName);

            await sender.SendMessageAsync(message);
            return true;
        }
    }
}
