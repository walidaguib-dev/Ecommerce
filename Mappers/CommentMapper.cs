using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class CommentMapper
    {
        public static Comments ToModel(this CreateCommentDto dto)
        {
            return new Comments
            {
                Content = dto.Content,
                CreatedAt = dto.CreatedAt,
                parentId = dto.parentId,
                UserId = dto.UserId,
            };
        }

        public static CommentDto ToDto(this Comments comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                parentId = comment.parentId,
                UserId = comment.UserId,
                username = comment.user.UserName,
                repliesCount = comment.replies.ToList().Capacity,
                likes = comment.commentLikes.Select(cl => cl.ToDto()).ToList()
            };
        }
    }
}