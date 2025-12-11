using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Helpers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/commentlikes")]
    public class CommentLikesController(ICommentLikes _commentLikesService) : ControllerBase
    {
        private readonly ICommentLikes commentLikesService = _commentLikesService;

        [HttpPost]
        public async Task<IActionResult> LikeComment([FromBody] CreateCommentLike dto)
        {
            try
            {
                await commentLikesService.LikeComment(dto);
                return Created();
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
        [Route("{id:int}/{userId}")]
        public async Task<IActionResult> UnLike(int id, string userId)
        {
            var like = await commentLikesService.UnlikeComment(userId, id);
            if (like == null) return NotFound();
            return Ok("unliked");
        }
    }
}