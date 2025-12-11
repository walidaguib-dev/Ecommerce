using System.Security.Claims;
using Ecommerce.Dtos.User;
using Ecommerce.Helpers;
using Ecommerce.Repositories;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class UserController(IUser userRepo) : ControllerBase
    {
        private readonly IUser _userRepo = userRepo;

        [HttpPost("SignUp")]
        [EnableRateLimiting(policyName: "fixed")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userRepo.CreateUser(dto);
                if (user == null) return BadRequest("User credentials are invalid!");
                return Created();
            }
            catch (ValidationException e)
            {

                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("SignIn")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userRepo.login(dto);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("verifyUser")]
        public async Task<IActionResult> verifyUser(string userId, int code)
        {
            var result = await _userRepo.VerifyUser(userId, code);
            if (result == null) return NotFound("verification code or user are not found!");
            return Ok("User account confirmed!");
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var result = await _userRepo.DeleteUser(userId);
            if (result == null) return NotFound();
            return Ok("user deleted!");
        }


        [HttpPatch()]
        [Route("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var result = await _userRepo.UpdateUser(userId, dto);
                if (result == null) return NotFound();
                return Ok("User updated");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("GenerateToken/{userId}")]
        public async Task<IActionResult> GenerateToken([FromRoute] string userId)
        {
            try
            {
                var result = await _userRepo.GenerateResetPasswordToken(userId);
                if (result == null) return NotFound();
                return Ok("Token has been sent, check you inbox!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("ResetPassword/{userId}")]
        public async Task<IActionResult> ResetPassword([FromRoute] string userId, [FromBody] PasswordDto dto)
        {
            try
            {
                var result = await _userRepo.PasswordReset(userId, dto);
                if (result == null) return NotFound("User Not Found!");
                return Ok("password updated!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }






    }
}
