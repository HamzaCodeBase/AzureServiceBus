using Azure.Messaging.ServiceBus;
using SB.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SB.Receiver
{
    internal class Program
    {
        const string connectionString = "";
        const string queueName = "";
        static async Task Main(string[] args)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            while (true)
            {
                ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5));
                Student student = JsonSerializer.Deserialize<Student>(message.Body);
                Console.WriteLine($"Received Message : {student.Id} \t {student.Name}");
                await receiver.CompleteMessageAsync(message);
            }
        }
    }
}
