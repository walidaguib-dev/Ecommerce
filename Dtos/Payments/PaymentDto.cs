using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Helpers;

namespace Ecommerce.Dtos.Payments
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public string Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public int orderId { get; set; }
    }
}