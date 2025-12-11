using Ecommerce.Dtos.Followers;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class FollowingMapper
    {
        public static Follower ToModel(this FollowDto dto)
        {
            return new Follower
            {
                UserId = dto.userId,
                FollowerId = dto.followerId
            };
        }

        public static FollowerDto ToFollowerDto(this Follower follower)
        {
            return new FollowerDto
            {
                followerId = follower.FollowerId,
                UserName = follower.follower.UserName


            };
        }
        public static FollowingDto ToFollowingDto(this Following following)
        {
            return new FollowingDto
            {
                followingId = following.followingId,
                username = following.following.UserName,



            };
        }
    }
}
