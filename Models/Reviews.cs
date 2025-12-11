namespace Ecommerce.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public int rating { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int? commentId { get; set; }
        public Comments comment { get; set; }
    }
}
