using Ecommerce.Dtos.Category;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface ICategory
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<Category?> GetCategoryAsync(int id);
        public Task<Category> CreateCategory(CreateCategoryDto dto);
        public Task<Category?> UpdateCategory(int id, CreateCategoryDto dto);
        public Task<Category?> DeleteCategory(int id);
    }
}
