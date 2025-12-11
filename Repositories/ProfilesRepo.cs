using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.Profile;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProfilesRepo
    (
        ApplicationDBContext _context,
        ICache _CacheService,
        [FromKeyedServices("createProfile")] IValidator<CreateProfileDto> _CreateProfileValidator,
        [FromKeyedServices("UpdateProfile")] IValidator<UpdateProfileDto> _UpdateProfileValidator,
        UserManager<User> _userManager
    ) : IProfile
    {
        private readonly ApplicationDBContext context = _context;
        private readonly ICache CacheService = _CacheService;
        private readonly IValidator<CreateProfileDto> CreateProfileValidator = _CreateProfileValidator;
        private readonly IValidator<UpdateProfileDto> UpdateProfileValidator = _UpdateProfileValidator;
        private UserManager<User> userManager = _userManager;

        public async Task<Profiles> CreateProfile(CreateProfileDto dto)
        {
            var result = CreateProfileValidator.Validate(dto);
            if (result.IsValid)
            {
                Profiles profile = dto.ToModel();
                User user = await userManager.Users.Where(u => u.Id == dto.userId).FirstAsync();
                await context.profiles.AddAsync(profile);
                user.ProfileId = profile.Id;
                await userManager.UpdateAsync(user);
                await context.SaveChangesAsync();
                await CacheService.RemoveCaching("profile");
                return profile;

            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Profiles?> GetProfile(string userId)
        {
            string key = "profile";
            var cachedProfile = await CacheService.GetFromCacheAsync<Profiles>(key);
            if (cachedProfile != null) return cachedProfile;
            var profile = await context.profiles
            .Include(p => p.user)
            .Include(p => p.file)
            .Where(p => p.userId == userId)
            .FirstAsync();
            if (profile == null) return null;
            await CacheService.SetAsync(key, profile);
            return profile;
        }

        public async Task<Profiles> UpdateProfile(int id, UpdateProfileDto dto)
        {
            var result = UpdateProfileValidator.Validate(dto);
            if (result.IsValid)
            {
                var profile = await context.profiles
                .Include(p => p.user)
                .Include(p => p.file)
                .Where(p => p.Id == id)
                .FirstAsync();

                profile.first_name = dto.first_name;
                profile.last_name = dto.last_name;
                profile.age = dto.age;
                profile.Gender = dto.Gender;
                profile.country = dto.country;
                profile.city = dto.city;
                profile.ZipCode = dto.ZipCode;
                profile.fileId = dto.fileId == null ? 1 : dto.fileId;
                await context.SaveChangesAsync();
                await CacheService.RemoveCaching("profile");
                return profile;

            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}