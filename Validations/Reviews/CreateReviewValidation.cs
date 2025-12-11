using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Reviews;
using FluentValidation;

namespace Ecommerce.Validations.Reviews
{
    public class CreateReviewValidation : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidation()
        {
            RuleFor(r => r.rating).NotNull().GreaterThan(0);
            RuleFor(r => r.userId).NotNull().NotEmpty();
            RuleFor(r => r.ProductId).NotNull().GreaterThan(0);
        }
    }
}