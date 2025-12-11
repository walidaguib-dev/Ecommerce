using Ecommerce.Models;

namespace Ecommerce.Dtos.Messages
{
    public class CreateMessage
    {
        public string? content { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int? fileId { get; set; }
        public MediaModel media { get; set; }
        public string senderId { get; set; }
        
        public string receiverId { get; set; }
        
    }
}
