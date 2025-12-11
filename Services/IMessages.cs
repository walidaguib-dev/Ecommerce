using Ecommerce.Dtos.Messages;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IMessages
    {
        public Task<IEnumerable<Messages>> GetMessagesAsync(string userId , string receiverId);
        public Task<Messages?> GetMessagesAsync(int id);
        public Task<Messages> SendMessageAsync(Messages message);
        public Task<Messages?> UpdateMessageAsync(int id, UpdateMessage message);
        public Task<Messages?> DeleteMessageAsync(int id);

    }
}
