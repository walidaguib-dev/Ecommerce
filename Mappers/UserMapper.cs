using Ecommerce.Dtos.User;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class UserMapper
    {
        public static User toUser(this RegisterDto dto)
        {
            return new User
            {
                Email = dto.email,
                UserName = dto.username,
                role = dto.role

            };
        }
    }
}
