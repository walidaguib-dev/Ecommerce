using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Payments;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IPayment
    {
        public Task<Payments?> GetPayments(int paymentId);
        public Task<Payments> InitiatePayment(InitiatePaymentDto dto);
        public Task<Payments?> ConfirmPayment(int paymentId);
        public Task<Payments?> CancelPayment(int paymentId);
        public Task<Payments?> RefundPayment(int paymentId);
    }
}