using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products;
using Ecommerce.Filters;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProduct
    {
        public Task<IEnumerable<Products>> GetProducts(QueryFilters query);
        public Task<IEnumerable<Products>> GetProducts(string userId, QueryFilters query);
        public Task<Products?> GetProduct(int id);
        public Task<Products?> AddProduct(CreateProductDto dto);
        public Task<Products?> UpdateProduct(int id, UpdateProductDto dto);
        public Task<Products?> DeletePoduct(int id);
    }
}