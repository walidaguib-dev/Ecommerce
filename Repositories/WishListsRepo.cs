using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.WishLists;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class WishListsRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("addItem")] IValidator<AddItemDto> _validator,
        ICache _cacheService
    ) : IWishList
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<AddItemDto> validator = _validator;
        private readonly ICache cacheService = _cacheService;
        public async Task<Wishlists> AddToCart(AddItemDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var item = dto.ToModel();
                await context.wishlists.AddAsync(item);
                await context.SaveChangesAsync();
                await cacheService.RemoveCaching("cart");
                return item;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Wishlists?> DeleteFromCart(int id)
        {
            var item = await context.wishlists.Where(w => w.Id == id).FirstAsync();
            if (item == null) return null;
            context.wishlists.Remove(item);
            await context.SaveChangesAsync();
            await cacheService.RemoveCaching("cart");
            return item;
        }

        public async Task<List<Wishlists>> GetAll(string userId)
        {
            string key = "cart";
            var cachedCart = await cacheService.GetFromCacheAsync<List<Wishlists>>(key);
            if (!cachedCart.IsNullOrEmpty()) return cachedCart;
            var items = await context.wishlists
            .Include(w => w.user)
            .Include(w => w.Product)
            .Where(w => w.userId == userId).ToListAsync();
            await cacheService.SetAsync(key, items);
            return items;
        }
    }
}