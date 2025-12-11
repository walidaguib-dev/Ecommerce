using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Products.Variants
{
    public class CreateProductVariant
    {
        public string size { get; set; }
        public List<string> colors { get; set; } = [];
        public int quantity { get; set; }
        public int ProductId { get; set; }
    }
}