using Ecommerce.Data;
using Ecommerce.Dtos.Messages;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class MessagesRepo(ApplicationDBContext _context) : IMessages
    {
        private readonly ApplicationDBContext context = _context;
        public async Task<Messages?> DeleteMessageAsync(int id)
        {
            var message = await context.messages.FindAsync(id);
            if (message == null) return null;
            context.messages.Remove(message);
            await context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<Messages>> GetMessagesAsync(string userId, string receiverId)
        {
            return await context.messages
                .Include(m => m.media)
                .Include(m => m.sender)
                .Include(m => m.receiver)
                .Where(m => m.senderId == userId && m.receiverId == receiverId)
                .ToListAsync();
        }

        public async Task<Messages?> GetMessagesAsync(int id)
        {
            var message = await context.messages
                .Include(m => m.media)
                .Include(m => m.sender )
                .Include(m => m.receiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null) return null;
            return message;
        }

        public Task<Messages> SendMessageAsync(Messages message)
        {
            throw new NotImplementedException();
        }

        public Task<Messages?> UpdateMessageAsync(int id, UpdateMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
