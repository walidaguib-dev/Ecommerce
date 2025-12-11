using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/ProductFiles")]
    public class ProductFilesController(IProductFiles _productFilesService) : ControllerBase
    {
        private readonly IProductFiles productFilesService = _productFilesService;

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllProductFiles([FromRoute] int id)
        {
            var productFiles = await productFilesService.GetProductFiles(id);
            var productFile = productFiles.Select(pf => pf.ToDto());
            return Ok(productFile);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> CreateProductFile([FromBody] CreateProductFile productFile, [FromRoute] string userId)
        {
            try
            {
                var ProductFile = await productFilesService.CreateProductFiles(productFile, userId);
                if (ProductFile == null) return NotFound();
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
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
    }
}