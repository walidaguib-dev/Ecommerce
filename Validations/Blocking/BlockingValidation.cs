using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Blocking;
using FluentValidation;

namespace Ecommerce.Validations.Blocking
{
    public class BlockingValidation : AbstractValidator<BlockUserDto>
    {
        public BlockingValidation()
        {
            RuleFor(b => b.UserId)
            .NotNull().NotEmpty();
            RuleFor(b => b.BlockedUserId).NotNull().NotEmpty();
        }
    }
}