using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Filters;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController(IReviews _reviewsRepo) : ControllerBase
    {
        private readonly IReviews reviewsRepo = _reviewsRepo;

        [HttpGet]
        [Route("{productId:int}")]
        public async Task<IActionResult> GetReviews([FromRoute] int productId, [FromQuery] QueryFilters query)
        {
            var reviews = await reviewsRepo.GetAllReviewsAsync(productId, query);
            var review = reviews.Select(r => r.ToDto());
            return Ok(review);
        }

        [HttpGet]
        [Route("review/{id:int}")]
        public async Task<IActionResult> GetReview([FromRoute] int id)
        {
            var review = await reviewsRepo.GetReview(id);
            if (review == null) return NotFound();
            return Ok(review.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            try
            {
                var result = await reviewsRepo.CreateReview(dto);
                if (result == null) return NotFound();
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromRoute] int ReviewId, [FromBody] UpdateReviewDto dto)
        {
            try
            {
                var result = await reviewsRepo.UpdateReview(ReviewId, dto);
                if (result == null) return NotFound();
                return Ok("Review updated");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var result = await reviewsRepo.DeleteReview(id);
            if (result == null) return NotFound();
            return Ok("Review deleted!");
        }
    }
}