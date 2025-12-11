namespace Ecommerce.Models
{
    public class Wishlists
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }

    }
}
