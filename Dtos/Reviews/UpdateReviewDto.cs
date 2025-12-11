using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Reviews
{
    public class UpdateReviewDto
    {
        public int rating { get; set; }
        public int? commentId { get; set; }
    }
}