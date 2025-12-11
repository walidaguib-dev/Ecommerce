namespace Ecommerce.Models
{
    public class Order_Product
    {
        public int productId { get; set; }
        public Products product { get; set; }
        public int orderId { get; set; }
        public Orders order { get; set; }
    }
}
