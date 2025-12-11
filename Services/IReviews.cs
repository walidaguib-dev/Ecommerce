using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Filters;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IReviews
    {
        public Task<ICollection<Reviews>> GetAllReviewsAsync(int productId, QueryFilters query);
        public Task<Reviews?> GetReview(int id);
        public Task<Reviews?> CreateReview(CreateReviewDto dto);
        public Task<Reviews?> UpdateReview(int id, UpdateReviewDto dto);
        public Task<Reviews?> DeleteReview(int id);
    }
}