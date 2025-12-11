using System.Security.Policy;
using Ecommerce.Data;
using Ecommerce.Dtos.User;

using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class UserRepo(
        UserManager<User> _manager,
        SignInManager<User> _signin,
        [FromKeyedServices("register")] IValidator<RegisterDto> RegisterValidator,
        [FromKeyedServices("login")] IValidator<LoginDto> LoginValidator,
        [FromKeyedServices("updateUser")] IValidator<UpdateUserDto> _UpdateUserValidator,
        IToken tokenService,
        IVerification _verification,
        ApplicationDBContext _context
        ) : IUser
    {
        private readonly UserManager<User> manager = _manager;
        private readonly SignInManager<User> signInManager = _signin;
        private readonly IValidator<RegisterDto> _RegisterValidator = RegisterValidator;
        private readonly IValidator<LoginDto> _LoginValidator = LoginValidator;
        private readonly IValidator<UpdateUserDto> UpdateUserValidator = _UpdateUserValidator;
        private readonly IToken _tokenService = tokenService;
        private readonly IVerification verification = _verification;
        private readonly ApplicationDBContext context = _context;



        public async Task<User?> CreateUser(RegisterDto dto)
        {
            var result = _RegisterValidator.Validate(dto);
            if (result.IsValid)
            {
                var user = new User
                {
                    UserName = dto.username,
                    Email = dto.email,
                    role = dto.role
                };
                Console.WriteLine($"{user.role}");

                var model = await manager.CreateAsync(user, dto.password);
                if (model.Succeeded)
                {
                    if (dto.role == Helpers.Role.Admin)
                    {
                        await manager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        await manager.AddToRoleAsync(user, "user");
                    }


                    // send email verification
                    int code = verification.GenerateCode();
                    await verification.SendVerificationEmail(dto.email, "", code.ToString());
                    await verification.CreateVerification(new EmailVerification
                    {
                        code = code,
                        userId = user.Id
                    });

                }

                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<User?> DeleteUser(string userId)
        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (user == null) return null;
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<string?> GenerateResetPasswordToken(string userId)
        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (user == null) return null;
            string token = await manager.GeneratePasswordResetTokenAsync(user);
            string numericString = new string(token.Where(char.IsDigit).ToArray()).Substring(0, 5);
            await verification.SendVerificationEmail(user.Email, "", token);
            await verification.CreateVerification(new EmailVerification
            {
                code = int.Parse(numericString),
                userId = user.Id
            });

            await context.SaveChangesAsync();
            return token;
        }





        public async Task<NewUser?> login(LoginDto dto)
        {
            var result = _LoginValidator.Validate(dto);
            if (result.IsValid)
            {
                var user = await manager.Users

                    .FirstOrDefaultAsync(u => u.UserName == dto.username);
                if (user == null) return null;
                var status = await signInManager.CheckPasswordSignInAsync(user, dto.password, false);
                if (status.Succeeded)
                {
                    var role = user.role == Helpers.Role.Admin ? "Admin" : "User";
                    return new NewUser { Id = user.Id, token = _tokenService.createToken(user, role), role = user.role.ToString() };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<User?> PasswordReset(string userId, PasswordDto dto)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null) return null;
            var result = await manager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new Exception("Cannot reset password , try again!");
            }
        }



        public async Task<User?> UpdateUser(string userId, UpdateUserDto dto)

        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (user == null) return null;
            var result = UpdateUserValidator.Validate(dto);
            if (result.IsValid)
            {
                if (user.Email != dto.email)
                {
                    user.Email = dto.email;
                    user.EmailConfirmed = false;
                    int code = verification.GenerateCode();
                    await verification.SendVerificationEmail(dto.email, "", code.ToString());
                    await verification.CreateVerification(new EmailVerification
                    {
                        code = code,
                        userId = userId,
                    });

                }
                user.UserName = dto.username;
                await context.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
        public async Task<bool?> VerifyUser(string userId, int verificationCode)
        {
            var verification = await context.emailVerifications.Where(e => e.code == verificationCode).FirstAsync();

            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (verification == null || user == null) return null;

            if (verification.code == verificationCode)
            {
                user.EmailConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
