using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IVariants
    {
        public Task<List<ProductsVariants>> getAll(int productId);
        public Task<ProductsVariants?> GetVariant(int id);
        public Task<ProductsVariants?> CreateVariant(CreateProductVariant variant, string userId);
        public Task<ProductsVariants?> UpdateVariant(int id, UpdateProductVariant variant);
        public Task<ProductsVariants?> DeleteVariant(int id);
    }
}