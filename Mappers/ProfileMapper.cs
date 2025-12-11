using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Profile;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ProfileMapper
    {
        public static Profiles ToModel(this CreateProfileDto dto)
        {
            return new Profiles
            {
                first_name = dto.first_name,
                last_name = dto.last_name,
                Gender = dto.Gender,
                age = dto.age,
                country = dto.country,
                city = dto.city,
                ZipCode = dto.ZipCode,
                fileId = dto.fileId == null ? 1 : dto.fileId,
                userId = dto.userId
            };
        }

        public static ProfileDto ToDto(this Profiles profile)
        {
            return new ProfileDto
            {
                Id = profile.Id,
                first_name = profile.first_name,
                last_name = profile.last_name,
                age = profile.age,
                Gender = profile.Gender,
                country = profile.country,
                city = profile.city,
                fileId = profile.fileId,
                ZipCode = profile.ZipCode,
                Image = profile.file.file,
                userId = profile.userId

            };
        }
    }
}