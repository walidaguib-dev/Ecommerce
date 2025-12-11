
using Ecommerce.Data;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CommentlikesRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("commentLike")] IValidator<CreateCommentLike> _validator
    )
     : ICommentLikes
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateCommentLike> validator = _validator;
        public async Task<CommentLikes?> LikeComment(CreateCommentLike dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                CommentLikes like = dto.ToModel();
                await context.commentLikes.AddAsync(like);
                await context.SaveChangesAsync();
                return like;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<CommentLikes?> UnlikeComment(string userId, int commentId)
        {
            var like = await context.commentLikes
                    .Where(cl => cl.UserId == userId && cl.CommentId == commentId).FirstAsync();
            if (like == null) return null;
            context.commentLikes.Remove(like);
            await context.SaveChangesAsync();
            return like;
        }
    }
}