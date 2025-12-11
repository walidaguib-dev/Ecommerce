using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products;
using FluentValidation;

namespace Ecommerce.Validations.Products
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidation()
        {
            RuleFor(p => p.Name)
             .NotNull().NotEmpty();
            RuleFor(p => p.Description)
            .NotNull().NotEmpty();
            RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(1).NotNull();
            RuleFor(p => p.UpdateAt)
            .NotNull().NotEmpty();
        }
    }
}