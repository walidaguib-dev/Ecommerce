using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using FluentValidation;

namespace Ecommerce.Validations.Comments
{
    public class UpdateCommentValidation : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentValidation()
        {
            RuleFor(c => c.content).NotNull().NotEmpty().MaximumLength(255);
        }
    }
}