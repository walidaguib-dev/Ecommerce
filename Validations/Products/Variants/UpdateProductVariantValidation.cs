using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Products.Variants;
using FluentValidation;

namespace Ecommerce.Validations.Products.Variants
{
    public class UpdateProductVariantValidation : AbstractValidator<UpdateProductVariant>
    {
        public UpdateProductVariantValidation()
        {
            RuleFor(p => p.size).NotEmpty().NotNull();
            RuleFor(p => p.colors).NotNull();
            RuleFor(p => p.quantity).NotNull().GreaterThan(-1);
        }
    }
}