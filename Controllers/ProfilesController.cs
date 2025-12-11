using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Profile;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController(IProfile _profileService) : ControllerBase
    {
        private readonly IProfile profileService = _profileService;

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto dto)
        {
            try
            {
                await profileService.CreateProfile(dto);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetProfile([FromRoute] string userId)
        {
            var profile = await profileService.GetProfile(userId);
            if (profile == null) return NotFound();
            return Ok(profile.ToDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] UpdateProfileDto dto)
        {
            try
            {
                await profileService.UpdateProfile(id, dto);
                return Ok("Profile updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}