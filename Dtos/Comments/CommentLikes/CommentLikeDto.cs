using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Comments.CommentLikes
{
    public class CommentLikeDto
    {
        public string UserId { get; set; }
        public string username { get; set; }
        public int CommentId { get; set; }

    }
}