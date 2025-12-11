using Ecommerce.Dtos.Comments;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController(IComments _commentsRepo) : ControllerBase
    {
        private readonly IComments commentsRepo = _commentsRepo;

        [HttpGet]
        public async Task<IActionResult> getAllComments()
        {
            var comments = await commentsRepo.GetAllComments();

            var comment = comments.Select(c => c.ToDto());
            return Ok(comment);
        }

        [HttpGet]
        [Route("replies/{id:int}")]
        public async Task<IActionResult> getAllReplies([FromRoute] int id)
        {
            var replies = await commentsRepo.GetAllReplies(id);
            var reply = replies.Select(r => r.ToDto());
            return Ok(reply);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await commentsRepo.GetComment(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToDto());
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
        {
            try
            {
                await commentsRepo.CreateComment(dto);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                }
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto dto)
        {
            try
            {
                var comment = await commentsRepo.UpdateComment(id, dto);
                if (comment == null) return NotFound();
                return Ok("Comment updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                }
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await commentsRepo.DeleteComment(id);
            if (comment == null) return NotFound();
            return Ok("comment Deleted!");
        }
    }
}