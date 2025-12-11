using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using FluentValidation;

namespace Ecommerce.Validations.Comments
{
    public class CreateCommentValidation : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidation()
        {
            RuleFor(c => c.Content)
            .NotEmpty().NotNull().MaximumLength(255);
            RuleFor(c => c.UserId).NotEmpty().NotNull();
        }
    }
}