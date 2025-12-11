using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IToken
    {
        public string createToken(User user, string role);
    }
}
