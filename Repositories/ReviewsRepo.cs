using Ecommerce.Data;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Filters;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class ReviewsRepo
    (
        ApplicationDBContext _context,
        ICache _cache,
        [FromKeyedServices("createReview")] IValidator<CreateReviewDto> _CreateReviewValidator,
        [FromKeyedServices("updateReview")] IValidator<UpdateReviewDto> _UpdateReviewValidator
    ) : IReviews
    {
        private readonly ApplicationDBContext context = _context;
        private readonly ICache cache = _cache;
        private readonly IValidator<CreateReviewDto> CreateReviewValidator = _CreateReviewValidator;
        private readonly IValidator<UpdateReviewDto> UpdateReviewValidator = _UpdateReviewValidator;

        public async Task<Reviews?> CreateReview(CreateReviewDto dto)
        {
            var user = await context.Users.Where(u => u.Id == dto.userId).FirstAsync();
            if (user == null) return null;
            if (user.role == Helpers.Role.User)
            {
                var result = CreateReviewValidator.Validate(dto);
                if (result.IsValid)
                {
                    var review = dto.ToModel();
                    await context.reviews.AddAsync(review);
                    await context.SaveChangesAsync();
                    return review;
                }
                else
                {
                    throw new ValidationException(result.Errors);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }

        public async Task<Reviews?> DeleteReview(int id)
        {
            var review = await context.reviews.Where(r => r.Id == id).FirstAsync();
            if (review == null) return null;
            context.reviews.Remove(review);
            await context.SaveChangesAsync();
            return review;
        }

        public async Task<ICollection<Reviews>> GetAllReviewsAsync(int productId, QueryFilters query)
        {
            // string key = $"reviews_{productId}_{query.SortBy}_{query.IsDescending}_{query.Limit}_{query.PageNumber}";
            // var cachedReviews = await cache.GetFromCacheAsync<ICollection<Reviews>>(key);
            // if (!cachedReviews.IsNullOrEmpty()) return cachedReviews;
            var reviews = context.reviews
            .Include(r => r.user)
            .Include(r => r.Product)
            .Include(r => r.comment)
            .Where(r => r.ProductId == productId).AsQueryable();

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    reviews = query.IsDescending ?
                               reviews.OrderByDescending(r => r.Id) :
                               reviews.OrderBy(r => r.Id);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedReviews = await reviews.Skip(skipNumber).Take(query.Limit).ToListAsync();
            // await cache.SetAsync(key, pagedReviews);
            return pagedReviews;
        }

        public async Task<Reviews?> GetReview(int id)
        {
            var review = await context.reviews
            .Include(r => r.user)
            .Include(r => r.Product)
            .Include(r => r.comment)
            .Where(r => r.Id == id).FirstAsync();
            if (review == null) return null;
            return review;
        }

        public async Task<Reviews?> UpdateReview(int id, UpdateReviewDto dto)
        {
            var result = UpdateReviewValidator.Validate(dto);
            if (result.IsValid)
            {
                var review = await context.reviews.Where(r => r.Id == id).FirstAsync();
                if (review == null) return null;
                review.rating = dto.rating;
                review.commentId = dto.commentId;
                await context.SaveChangesAsync();
                return review;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}