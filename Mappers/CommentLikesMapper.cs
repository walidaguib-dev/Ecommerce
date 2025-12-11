using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class CommentLikesMapper
    {
        public static CommentLikes ToModel(this CreateCommentLike commentLike)
        {
            return new CommentLikes
            {
                CommentId = commentLike.CommentId,
                UserId = commentLike.UserId
            };
        }

        public static CommentLikeDto ToDto(this CommentLikes commentLikes)
        {
            return new CommentLikeDto
            {
                UserId = commentLikes.UserId,
                username = commentLikes.user.UserName,
                CommentId = commentLikes.CommentId,
            };
        }
    }
}