using Ecommerce.Helpers;

namespace Ecommerce.Dtos.User
{
    public class RegisterDto
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
    }
}
