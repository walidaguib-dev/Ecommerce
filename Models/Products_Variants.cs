using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class ProductsVariants
    {
        public int Id { get; set; }
        public string size { get; set; }
        public List<string> colors { get; set; }
        public int quantity { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
