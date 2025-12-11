namespace Ecommerce.Models
{
    public class BlockedUsers
    {
        public string userId { get; set; }
        public User user { get; set; }
        public string blockedUserId { get; set; }
        public User blockedUser { get; set; }
    }
}
