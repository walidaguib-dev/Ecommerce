using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products.Variants;
using FluentValidation;

namespace Ecommerce.Validations.Products.Variants
{
    public class CreateProductVariantValidation : AbstractValidator<CreateProductVariant>
    {
        public CreateProductVariantValidation()
        {
            RuleFor(p => p.size).NotEmpty().NotNull();
            RuleFor(p => p.colors).NotNull();
            RuleFor(p => p.quantity).NotNull().GreaterThan(-1);
            RuleFor(p => p.ProductId).NotNull().GreaterThan(0);
        }
    }
}