using Ecommerce.Dtos.Blocking;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class BlockingMapper
    {

        public static BlockedUsers ToModel(this BlockUserDto dto)
        {
            return new BlockedUsers
            {
                userId = dto.UserId,
                blockedUserId = dto.BlockedUserId
            };
        }

        public static BlockedUserDto ToDto(this BlockedUsers user)
        {
            return new BlockedUserDto
            {
                BlockedUserId = user.blockedUserId,
                Username = user.blockedUser.UserName,

            };
        }
    }
}