using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Reviews
{
    public class CreateReviewDto
    {
        public int rating { get; set; }
        public string userId { get; set; }
        public int ProductId { get; set; }
        public int? commentId { get; set; }
    }
}