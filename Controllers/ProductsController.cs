
using Ecommerce.Dtos.Products;
using Ecommerce.Filters;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/Products")]

    public class ProductsController(IProduct _productService) : ControllerBase
    {
        private readonly IProduct productService = _productService;


        [HttpGet]

        public async Task<IActionResult> GetProducts([FromQuery] QueryFilters query)
        {
            var products = await productService.GetProducts(query);
            var product = products.Select(p => p.ToDto());
            return Ok(product);
        }

        [HttpGet]
        [Route("{userId}")]


        public async Task<IActionResult> GetProducts([FromRoute] string userId, [FromQuery] QueryFilters query)
        {
            var products = await productService.GetProducts(userId, query);
            var product = products.Select(p => p.ToDto());
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await productService.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product.ToDto());
        }


        [HttpPost]

        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto)
        {
            try
            {
                var product = await productService.AddProduct(dto);
                if (product == null) return NotFound("user not found!");
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var product = await productService.UpdateProduct(id, dto);
                if (product == null) return NotFound();
                return Ok("Product Updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized();
            }
            catch (DbUpdateException ex)
            {
                // Log or print the inner exception for more details
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await productService.DeletePoduct(id);
            if (product == null) return NotFound();
            return Ok("Product deleted!");
        }

    }
}