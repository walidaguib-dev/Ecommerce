namespace Ecommerce.Models
{
    public class Follower
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FollowerId { get; set; }
        public User follower { get; set; }
    }
}
