using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/product_Variants")]
    public class ProductVariantsController(IVariants _variantsService) : ControllerBase
    {
        private readonly IVariants variantsService = _variantsService;

        [HttpGet]
        [Route("All/{productId:int}")]
        public async Task<IActionResult> GetAllVariants([FromRoute] int productId)
        {
            var variants = await variantsService.getAll(productId);
            var variant = variants.Select(v => v.ToDto());
            return Ok(variant);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetVariant([FromRoute] int id)
        {
            var variant = await variantsService.GetVariant(id);
            if (variant == null) return NotFound();
            return Ok(variant.ToDto());
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> CreateVariant([FromRoute] string userId, [FromBody] CreateProductVariant variant)
        {
            try
            {
                var result = await variantsService.CreateVariant(variant, userId);
                if (result == null) return NotFound();
                return Created();
            }
            catch (ValidationException v)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = v.Errors.Select(v => v.ErrorMessage)
                });
            }
            catch (UnauthorizedAccessException)
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
        public async Task<IActionResult> UpdateVariant([FromRoute] int id, [FromBody] UpdateProductVariant variant)
        {
            try
            {
                var result = await variantsService.UpdateVariant(id, variant);
                if (result == null) return NotFound();
                return Ok("Variant updated");
            }
            catch (ValidationException v)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = v.Errors.Select(v => v.ErrorMessage)
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVariant([FromRoute] int id)
        {
            var variant = await variantsService.DeleteVariant(id);
            if (variant == null) return NotFound();
            return Ok("variant deleted!");
        }
    }
}