using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ProductMapper
    {
        public static Products ToModel(this CreateProductDto dto)
        {
            return new Products
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = dto.createdAt,
                UpdateAt = null,
                userId = dto.userId
            };
        }

        public static ProductDto ToDto(this Products product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdateAt = product.UpdateAt,
                userId = product.userId,
                UserName = product.user.UserName
            };
        }
    }
}