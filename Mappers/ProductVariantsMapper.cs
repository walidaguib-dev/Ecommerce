using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ProductVariantsMapper
    {
        public static ProductsVariants ToModel(this CreateProductVariant variant)
        {
            return new ProductsVariants
            {
                size = variant.size,
                colors = variant.colors,
                quantity = variant.quantity,
                ProductId = variant.ProductId
            };
        }

        public static ProductVariantDto ToDto(this ProductsVariants variant)
        {
            return new ProductVariantDto
            {
                Id = variant.Id,
                size = variant.size,
                colors = variant.colors,
                quantity = variant.quantity,
                ProductId = variant.ProductId,
                productName = variant.Product.Name
            };
        }
    }
}