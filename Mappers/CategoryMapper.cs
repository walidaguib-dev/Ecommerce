using Ecommerce.Dtos.Category;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToCategory(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
