using SB.Shared;

namespace SB.Sender.Services
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(Student student, String QueueName);
    }
}
