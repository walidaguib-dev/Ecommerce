using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProductFiles
    {
        public Task<ProductFiles?> CreateProductFiles(CreateProductFile productFile, string userId);
        public Task<IEnumerable<ProductFiles>> GetProductFiles(int ProductId);
    }
}