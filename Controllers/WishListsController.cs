using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.WishLists;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/Cart")]
    public class WishListsController(IWishList _cartService) : ControllerBase
    {
        private readonly IWishList cartService = _cartService;

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAllItems([FromRoute] string userId)
        {
            var items = await cartService.GetAll(userId);
            var item = items.Select(i => i.ToDto());
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddItemDto dto)
        {
            try
            {
                await cartService.AddToCart(dto);
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

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await cartService.DeleteFromCart(id);
            if (item == null) return NotFound();
            return Ok("Item deleted!");
        }
    }
}