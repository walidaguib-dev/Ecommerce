using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.Payments;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class PaymentsRepo
    (
         ApplicationDBContext _context,
         [FromKeyedServices("initiatePayment")] IValidator<InitiatePaymentDto> _paymentsValidator
    ) : IPayment
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<InitiatePaymentDto> paymentsValidator = _paymentsValidator;
        public async Task<Payments?> CancelPayment(int paymentId)
        {
            var payment = await context.payments.Where(p => p.Id == paymentId).FirstAsync();
            if (payment == null) return null;
            payment.Status = Helpers.PaymentStatus.Cancelled;
            await context.SaveChangesAsync();
            return payment;
        }

        public Task<Payments?> ConfirmPayment(int paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<Payments?> GetPayments(int paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<Payments> InitiatePayment(InitiatePaymentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Payments?> RefundPayment(int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}