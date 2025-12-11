using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Followers
{
    public class FollowDto
    {
        public string userId { get; set; }
        public string followerId { get; set; }
    }
}
