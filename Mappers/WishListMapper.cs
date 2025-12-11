using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.WishLists;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class WishListMapper
    {
        public static Wishlists ToModel(this AddItemDto dto)
        {
            return new Wishlists
            {
                ProductId = dto.ProductId,
                userId = dto.userId
            };
        }

        public static WishListItemsDto ToDto(this Wishlists wishlists)
        {
            return new WishListItemsDto
            {
                Id = wishlists.Id,
                userId = wishlists.userId,
                username = wishlists.user.UserName,
                ProductId = wishlists.ProductId,
                ProductName = wishlists.Product.Name
            };
        }
    }
}