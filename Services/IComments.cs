using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IComments
    {
        public Task<List<Comments>> GetAllComments();
        public Task<List<Comments>> GetAllReplies(int commentId);
        public Task<Comments?> GetComment(int commentId);
        public Task<Comments> CreateComment(CreateCommentDto dto);
        public Task<Comments?> UpdateComment(int commentId, UpdateCommentDto dto);
        public Task<Comments?> DeleteComment(int commentId);
    }
}