using Ecommerce.Data;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class ProductsVariantsRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("createVariant")] IValidator<CreateProductVariant> _CreateVariantValidator,
        [FromKeyedServices("updateVariant")] IValidator<UpdateProductVariant> _UpdateVariantValidator,
        ICache _cacheService
    )
     : IVariants
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateProductVariant> CreateVariantValidator = _CreateVariantValidator;
        private readonly IValidator<UpdateProductVariant> UpdateVariantValidator = _UpdateVariantValidator;
        private readonly ICache cacheService = _cacheService;

        public async Task<ProductsVariants?> CreateVariant(CreateProductVariant variant, string userId)
        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (user == null) return null;
            if (user.role == Helpers.Role.Admin)
            {
                var result = CreateVariantValidator.Validate(variant);
                if (result.IsValid)
                {
                    var model = variant.ToModel();
                    await context.Variants.AddAsync(model);
                    await context.SaveChangesAsync();
                    await cacheService.RemoveCaching("variants");
                    return model;
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

        public async Task<ProductsVariants?> DeleteVariant(int id)
        {
            var variant = await context.Variants.Where(v => v.Id == id).FirstAsync();
            if (variant == null) return null;
            context.Variants.Remove(variant);
            await context.SaveChangesAsync();
            await cacheService.RemoveCaching("variants");
            return variant;

        }

        public async Task<List<ProductsVariants>> getAll(int productId)
        {
            string key = "variants";
            var cachedVariants = await cacheService.GetFromCacheAsync<List<ProductsVariants>>(key);
            if (!cachedVariants.IsNullOrEmpty()) return cachedVariants;
            var variants = await context.Variants
            .Include(v => v.Product)
            .Where(v => v.ProductId == productId).ToListAsync();
            await cacheService.SetAsync(key, variants);
            return variants;
        }

        public async Task<ProductsVariants?> GetVariant(int id)
        {
            var variant = await context.Variants
            .Include(v => v.Product)
            .Where(v => v.Id == id).FirstAsync();
            if (variant == null) return null;
            return variant;
        }

        public async Task<ProductsVariants?> UpdateVariant(int id, UpdateProductVariant variant)
        {
            var result = UpdateVariantValidator.Validate(variant);
            if (result.IsValid)
            {
                var v = await context.Variants
                                .Where(v => v.Id == id).FirstAsync();
                if (variant == null) return null;
                v.size = variant.size;
                v.colors = variant.colors;
                v.quantity = variant.quantity;
                await context.SaveChangesAsync();
                await cacheService.RemoveCaching("variants");
                return v;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}