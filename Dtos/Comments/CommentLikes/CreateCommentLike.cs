using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Comments.CommentLikes
{
    public class CreateCommentLike
    {
        public string UserId { get; set; }
        public int CommentId { get; set; }
    }
}