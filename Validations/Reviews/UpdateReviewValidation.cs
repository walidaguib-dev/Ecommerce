using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Reviews;
using FluentValidation;

namespace Ecommerce.Validations.Reviews
{
    public class UpdateReviewValidation : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidation()
        {
            RuleFor(r => r.rating).NotNull().GreaterThan(0);
        }
    }
}