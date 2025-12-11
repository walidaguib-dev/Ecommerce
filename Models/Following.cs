namespace Ecommerce.Models
{
    public class Following
    {
        public string followerId { get; set; }
        public User follower { get; set; }
        public string followingId { get; set; }
        public User following { get; set; }
    }
}
