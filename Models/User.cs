
using Ecommerce.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User : IdentityUser
    {
        public Role role { get; set; }
        public int? ProfileId { get; set; }
        public Profiles Profile { get; set; }
        public List<Orders> orders { get; set; } = [];
        public List<CommentLikes> commentLikes { get; set; } = [];
        public List<Payments> payments { get; set; } = [];
        public List<Follower> followers { get; set; } = [];
        public List<Following> followings { get; set; } = [];
        public List<BlockedUsers> blockedUsers { get; set; } = [];
    }
}
