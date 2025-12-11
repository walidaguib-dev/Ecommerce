using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ReviewsMapper
    {
        public static Reviews ToModel(this CreateReviewDto dto)
        {
            return new Reviews
            {
                rating = dto.rating,
                userId = dto.userId,
                ProductId = dto.ProductId,
                commentId = dto.commentId
            };
        }

        public static ReviewDto ToDto(this Reviews review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                rating = review.rating,
                userId = review.userId,
                username = review.user.UserName,
                ProductId = review.ProductId,
                product_name = review.Product.Name,
                // commentId = review.commentId,
                // content = review.comment.Content
            };
        }
    }
}