using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Products.Variants
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string size { get; set; }
        public List<string> colors { get; set; }
        public int quantity { get; set; }
        public int ProductId { get; set; }
        public string productName { get; set; }
    }
}