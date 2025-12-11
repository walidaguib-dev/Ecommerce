using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Followers
{
    public class FollowerDto
    {
        public string followerId { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
