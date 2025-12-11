using Ecommerce.Data;
using Ecommerce.Dtos.Category;
using Ecommerce.Hubs;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class CategoryRepo
        (
          ApplicationDBContext _context,
          [FromKeyedServices("category")] IValidator<CreateCategoryDto> _validator,
          ICache _cacheService,
          IHubContext<MessagesHub> _hub
        )
        : ICategory
    {

        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateCategoryDto> validator = _validator;
        private readonly ICache cacheService = _cacheService;
        private readonly IHubContext<MessagesHub> hub = _hub;
        public async Task<Category> CreateCategory(CreateCategoryDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var category = dto.ToCategory();
                await context.categories.AddAsync(category);
                await context.SaveChangesAsync();
                await hub.Clients.All.SendAsync("receiveMessage", category.Name);
                // await cacheService.RemoveCaching("categories");
                return category;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await context.categories.FindAsync(id);
            if (category == null) return null;
            context.categories.Remove(category);
            await context.SaveChangesAsync();
            await cacheService.RemoveCaching("categories");
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            string key = "categories";
            var cachedValues = await cacheService.GetFromCacheAsync<IEnumerable<Category>>(key);
            if (!cachedValues.IsNullOrEmpty()) return cachedValues;
            var categories = await context.categories.ToListAsync();
            await cacheService.SetAsync(key, categories);
            return categories;
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {

            var category = await context.categories.FindAsync(id);
            if (category == null) return null;
            return category;
        }

        public async Task<Category?> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var category = await context.categories.FindAsync(id);
                if (category == null) return null;
                category.Name = dto.Name;
                await context.SaveChangesAsync();
                return category;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
