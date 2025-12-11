using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Products
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateOnly? UpdateAt { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public string userId { get; set; }
    }
}