using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Payments;
using Ecommerce.Helpers;
using FluentValidation;

namespace Ecommerce.Validations.Payments
{
    public class InitiatePaymentValidation : AbstractValidator<InitiatePaymentDto>
    {
        public InitiatePaymentValidation()
        {
            RuleFor(p => p.amount).GreaterThan(0).NotNull();
            RuleFor(p => p.Method).NotEmpty().NotNull();
            RuleFor(p => p.userId).NotEmpty().NotNull();
            RuleFor(p => p.orderId).GreaterThan(0).NotNull();
        }
    }
}