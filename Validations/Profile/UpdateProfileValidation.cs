using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Profile;
using FluentValidation;

namespace Ecommerce.Validations.Profile
{
    public class UpdateProfileValidation : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileValidation()
        {
            RuleFor(p => p.first_name)
                .NotEmpty().NotNull();
            RuleFor(p => p.last_name)
                .NotEmpty().NotNull();
            RuleFor(p => p.age)
                .NotNull().GreaterThan(14);
            RuleFor(p => p.fileId)
                .GreaterThan(0).NotNull();
        }
    }
}