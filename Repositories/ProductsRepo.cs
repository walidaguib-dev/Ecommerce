using Ecommerce.Data;
using Ecommerce.Dtos.Products;
using Ecommerce.Filters;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class ProductsRepo
    (
        UserManager<User> _manager,
        ApplicationDBContext _context,
        ICache _cacheService,
        [FromKeyedServices("createProduct")] IValidator<CreateProductDto> _CreateProductValidator,
        [FromKeyedServices("updateProduct")] IValidator<UpdateProductDto> _UpdateProductValidator
    ) : IProduct
    {
        private readonly UserManager<User> manager = _manager;
        private readonly ApplicationDBContext context = _context;
        private readonly ICache cacheService = _cacheService;
        private readonly IValidator<CreateProductDto> CreateProductValidator = _CreateProductValidator;
        private readonly IValidator<UpdateProductDto> UpdateProductValidator = _UpdateProductValidator;

        public async Task<Products?> AddProduct(CreateProductDto dto)
        {
            var user = await manager.Users.FirstOrDefaultAsync(u => u.Id == dto.userId);
            if (user == null) return null;
            if (user.role == Helpers.Role.Admin)
            {
                var result = CreateProductValidator.Validate(dto);
                if (result.IsValid)
                {
                    Products product = dto.ToModel();
                    await context.products.AddAsync(product);
                    await context.SaveChangesAsync();
                    await cacheService.RemoveCaching("products");
                    await cacheService.RemoveByPattern("products");
                    return product;
                }
                else
                {
                    throw new ValidationException(result.Errors);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<Products?> DeletePoduct(int id)
        {
            var product = await context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            context.products.Remove(product);
            await context.SaveChangesAsync();
            await cacheService.RemoveCaching("products");
            await cacheService.RemoveByPattern("products");
            return product;
        }

        public async Task<Products?> GetProduct(int id)
        {

            var product = await context.products
            .Include(p => p.user)
            .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Products>> GetProducts(QueryFilters query)
        {
            string key = $"products_{query.PageNumber}_{query.Limit}_{query.SortBy}_{query.IsDescending}_{query.Name}";
            var cachedProducts = await cacheService.GetFromCacheAsync<IEnumerable<Products>>(key);
            if (!cachedProducts.IsNullOrEmpty()) return cachedProducts;
            var products = context.products
            .Include(p => p.user)
            .AsQueryable();
            if (!string.IsNullOrEmpty(query.Name) || !string.IsNullOrWhiteSpace(query.Name))
            {
                await cacheService.RemoveCaching("products");
                products = products.Where(f => f.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {
                await cacheService.RemoveCaching("products");
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ?
                                 products.OrderByDescending(f => f.Id)
                                 : products.OrderBy(f => f.Id);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ?
                                 products.OrderByDescending(f => f.Name)
                                 : products.OrderBy(f => f.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedproducts = await products.Skip(skipNumber).Take(query.Limit).ToListAsync();
            await cacheService.SetAsync(key, pagedproducts);
            return pagedproducts;
        }

        public async Task<IEnumerable<Products>> GetProducts(string userId, QueryFilters query)
        {
            string key = $"products_{userId}_{query.PageNumber}_{query.Limit}_{query.SortBy}_{query.IsDescending}_{query.Name}";
            var cachedProducts = await cacheService.GetFromCacheAsync<IEnumerable<Products>>(key);
            if (!cachedProducts.IsNullOrEmpty()) return cachedProducts;
            var products = context.products
            .Include(p => p.user)
            .AsQueryable();
            if (!string.IsNullOrEmpty(query.Name) || !string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(f => f.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ?
                                 products.OrderByDescending(f => f.Id)
                                 : products.OrderBy(f => f.Id);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ?
                                 products.OrderByDescending(f => f.Name)
                                 : products.OrderBy(f => f.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedproducts = await products.Skip(skipNumber).Take(query.Limit).ToListAsync();
            await cacheService.SetAsync(key, pagedproducts);
            return pagedproducts;
        }

        public async Task<Products?> UpdateProduct(int id, UpdateProductDto dto)
        {
            var user = await manager.Users.FirstOrDefaultAsync(u => u.Id == dto.userId);
            if (user == null) return null;
            if (user.role == Helpers.Role.Admin)
            {
                var result = UpdateProductValidator.Validate(dto);
                if (result.IsValid)
                {
                    var product = await context.products
                    .Include(p => p.user)
                    .FirstOrDefaultAsync(p => p.Id == id);
                    if (product == null) return null;
                    product.Name = dto.Name;
                    product.Description = dto.Description;
                    product.Price = dto.Price;
                    product.UpdateAt = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                    await cacheService.RemoveCaching("products");
                    await cacheService.RemoveByPattern("products");
                    return product;
                }
                else
                {
                    throw new ValidationException(result.Errors);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }
    }
}