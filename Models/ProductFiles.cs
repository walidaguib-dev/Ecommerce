namespace Ecommerce.Models
{
    public class ProductFiles
    {
        public int fileId { get; set; }
        public MediaModel file { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
