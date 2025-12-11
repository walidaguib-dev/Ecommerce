namespace Ecommerce.Models
{
    public class MediaModel
    {
        public int Id { get; set; }
        public string file { get; set; }
        public List<ProductFiles> productFiles { get; set; } = [];
    }
}
