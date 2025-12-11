using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products;
using FluentValidation;

namespace Ecommerce.Validations.Products
{
    public class CreateProductValidation : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidation()
        {
            RuleFor(p => p.Name)
            .NotNull().NotEmpty();
            RuleFor(p => p.Description)
            .NotNull().NotEmpty();
            RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(1).NotNull();
        }
    }
}