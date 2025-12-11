using Ecommerce.Helpers;

namespace Ecommerce.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateOnly Order_Date { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Order_Status { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public List<Order_Product> order_Products { get; set; } = [];
        public List<Payments> payments { get; set; } = [];
    }
}
