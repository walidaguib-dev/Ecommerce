using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments.CommentLikes;
using FluentValidation;

namespace Ecommerce.Validations.Comments.CommentLikes
{
    public class CommentLikesValidation : AbstractValidator<CreateCommentLike>
    {
        public CommentLikesValidation()
        {
            RuleFor(c => c.UserId)
            .NotNull().NotEmpty();
            RuleFor(c => c.CommentId)
            .NotNull().GreaterThan(0);
        }
    }
}