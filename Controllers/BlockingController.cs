using Ecommerce.Dtos.Blocking;
using Ecommerce.Filters;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/blocking")]
    public class BlockingController(IBlocking _blockingService) : ControllerBase
    {
        private readonly IBlocking blockingService = _blockingService;

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetBlockedUsers([FromRoute] string userId , [FromQuery] QueryFilters query)
        {
            var users = await blockingService.GetBlockedUsers(userId , query);
            var user = users.Select(u => u.ToDto());
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserDto dto)
        {
            try
            {
                var user = await blockingService.BlockUser(dto);
                if (user == null) return BadRequest("Something went wrong!");
                return Ok("User blocked!");
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

        [HttpDelete]
        public async Task<IActionResult> UnBlockUser([FromBody] BlockUserDto dto)
        {
            try
            {
                var user = await blockingService.UnblockUser(dto);
                if (user == null) return BadRequest("Something went wrong!");
                return Ok("User unblocked!");
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
    }
}