using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.ProuctFiles;
using FluentValidation;

namespace Ecommerce.Validations.ProductFiles
{
    public class ProductFilesValidation : AbstractValidator<CreateProductFile>
    {
        public ProductFilesValidation()
        {
            RuleFor(p => p.ProductId)
            .GreaterThan(0).NotNull();
            RuleFor(p => p.FileId)
            .GreaterThan(0).NotNull();
        }
    }
}