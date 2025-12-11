using Ecommerce.Models;

namespace Ecommerce.Dtos.Messages
{
    public class UpdateMessage
    {
        public string? content { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int? fileId { get; set; }
        public MediaModel media { get; set; }
    }
}
