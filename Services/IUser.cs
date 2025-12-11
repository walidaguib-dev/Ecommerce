using System.Security.Claims;
using Ecommerce.Dtos.User;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication;

namespace Ecommerce.Services
{
    public interface IUser
    {
        public Task<User?> CreateUser(RegisterDto dto);
        public Task<NewUser?> login(LoginDto dto);
        public Task<bool?> VerifyUser(string userId, int verificationCode);
        public Task<User?> UpdateUser(string userId, UpdateUserDto dto);
        public Task<User?> DeleteUser(string userId);
        public Task<string?> GenerateResetPasswordToken(string userId);

        public Task<User?> PasswordReset(string userId, PasswordDto dto);

    }
}
