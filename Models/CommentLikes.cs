namespace Ecommerce.Models
{
    public class CommentLikes
    {
        public string UserId { get; set; }

        public int CommentId { get; set; }

        public User user { get; set; }

        public Comments comment { get; set; }
    }
}
