using Ecommerce.Dtos.Category;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(ICategory _categoryService) : ControllerBase
    {
        private readonly ICategory categoryService = _categoryService;


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryService.GetCategoriesAsync();
            var category = categories.Select(c => c.ToCategoryDto());
            return Ok(category);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category == null) return NotFound();
            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            
            try
            {
                var category = await categoryService.CreateCategory(dto);
                return Created();
            }catch (ValidationException e){
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id , [FromBody] CreateCategoryDto dto)
        {
            try
            {
                var category = await categoryService.UpdateCategory(id, dto);
                if(category == null ) return NotFound();
                return Ok("Category updated");
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
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await categoryService.DeleteCategory(id);
            if (category == null) return NotFound();
            return Ok("Category Deleted");
        }
    }
}
