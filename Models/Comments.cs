using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;


namespace Ecommerce.Models
{

    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
        public int? parentId { get; set; }
        public Comments parent { get; set; }
        public List<CommentLikes> commentLikes { get; set; } = [];
        public List<Comments> replies { get; set; } = [];

    }
}
