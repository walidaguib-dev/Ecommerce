using Ecommerce.Helpers;

namespace Ecommerce.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public string Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public int orderId { get; set; }
        public Orders order { get; set; }

    }
}
