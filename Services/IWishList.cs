using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.WishLists;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IWishList
    {
        public Task<List<Wishlists>> GetAll(string userId);
        public Task<Wishlists> AddToCart(AddItemDto dto);
        public Task<Wishlists?> DeleteFromCart(int id);
    }
}