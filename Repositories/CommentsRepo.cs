using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.Comments;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class CommentsRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("createComment")] IValidator<CreateCommentDto> _CreateCommentValidator,
        [FromKeyedServices("updateComment")] IValidator<UpdateCommentDto> _UpdateCommentValidator
    )
     : IComments
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateCommentDto> CreateCommentValidator = _CreateCommentValidator;
        private readonly IValidator<UpdateCommentDto> UpdateCommentValidator = _UpdateCommentValidator;

        public async Task<Comments> CreateComment(CreateCommentDto dto)
        {
            var result = CreateCommentValidator.Validate(dto);
            if (result.IsValid)
            {
                var model = dto.ToModel();
                await context.comments.AddAsync(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Comments?> DeleteComment(int commentId)
        {
            var comment = await context.comments.Where(c => c.Id == commentId).FirstAsync();
            if (comment == null) return null;
            context.comments.Remove(comment);
            await context.SaveChangesAsync();
            return comment;

        }

        public async Task<List<Comments>> GetAllComments()
        {

            var comments = await context.comments
            .Include(c => c.user)
            .Include(c => c.parent)
            .Include(c => c.commentLikes)
            .Include(c => c.replies)
            .Where(c => c.parentId == null)
            .ToListAsync();
            return comments;

        }

        public async Task<List<Comments>> GetAllReplies(int commentId)
        {

            var replies = await context.comments
            .Include(c => c.user)
            .Include(c => c.parent)
            .Where(r => r.parentId == commentId)
            .ToListAsync();
            return replies;

        }

        public async Task<Comments?> GetComment(int commentId)
        {
            var comment = await context.comments
            .Include(c => c.user)
            .Include(c => c.parent)
            .Where(c => c.Id == commentId)
            .FirstAsync();
            return comment;

        }


        public async Task<Comments?> UpdateComment(int commentId, UpdateCommentDto dto)
        {
            var result = UpdateCommentValidator.Validate(dto);
            if (result.IsValid)
            {
                var comment = await context.comments.Where(c => c.Id == commentId).FirstAsync();
                if (comment == null) return null;
                comment.Content = dto.content;
                await context.SaveChangesAsync();
                return comment;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}