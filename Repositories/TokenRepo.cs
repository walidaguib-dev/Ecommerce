using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Repositories
{
    public class TokenRepo : IToken
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey key;
        public TokenRepo(IConfiguration configuration)
        {
            _config = configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SignInKey"]));
        }
        public string createToken(User user, string role)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.GivenName, user.UserName),
                new(ClaimTypes.Role , role),
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMonths(2),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tk = tokenHandler.CreateToken(token);
            return tokenHandler.WriteToken(tk);
        }
    }
}
