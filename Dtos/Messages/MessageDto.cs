using Ecommerce.Models;

namespace Ecommerce.Dtos.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string? content { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int? fileId { get; set; }
        public string? filePath { get; set; }
        public string senderId { get; set; }
        public string SenderName { get; set; }
        public string receiverId { get; set; }
        public string ReceiverName { get; set; }

    }
}
