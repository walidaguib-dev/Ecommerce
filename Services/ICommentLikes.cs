using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface ICommentLikes
    {
        public Task<CommentLikes?> LikeComment(CreateCommentLike dto);
        public Task<CommentLikes?> UnlikeComment(string userId, int commentId);
    }
}