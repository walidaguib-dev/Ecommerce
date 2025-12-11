using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Profile;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProfile
    {
        public Task<Profiles?> GetProfile(string userId);
        public Task<Profiles> CreateProfile(CreateProfileDto dto);
        public Task<Profiles> UpdateProfile(int id, UpdateProfileDto dto);
    }
}