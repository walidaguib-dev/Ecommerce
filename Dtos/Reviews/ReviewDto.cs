using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int rating { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public int ProductId { get; set; }
        public string product_name { get; set; }
        public int? commentId { get; set; }
        public string content { get; set; }
    }
}