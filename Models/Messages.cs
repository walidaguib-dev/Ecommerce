namespace Ecommerce.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string? content { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int? fileId { get; set; }
        public MediaModel media { get; set; }
        public string senderId { get; set; }
        public User sender { get; set; }
        public string receiverId { get; set; }
        public User receiver { get; set; }
    }
}
