using Ecommerce.Data;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController(IMedia _media) : ControllerBase
    {
        private readonly IMedia media = _media;
        


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var resource = await media.UploadImage(file);
                if (resource == null) return NotFound();
                var model = await media.CreateMediaFile(new Dtos.MediaDtos.CreateFile { file = resource });
                return Ok(model.ToFileDto());
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> UploadMultiFiles(IFormFileCollection formFiles, [FromRoute] string userId)
        {
            try
            {
                List<string> files = await media.UploadFiles(formFiles, userId);
                ICollection<MediaModel> models = [];
                foreach (string file in files)
                {
                    try
                    {
                        var model = await media.CreateMediaFile(new Dtos.MediaDtos.CreateFile { file = file });
                        models.Add(model);
                    }
                    catch (ValidationException e)
                    {
                        return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                var mediaModel = models.Select(m => m.ToFileDto());
                return Ok(mediaModel);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }


        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFile([FromRoute] int id)
        {
            var file = await media.DeleteFile(id);
            if (file == null) return NotFound();
            return Ok("File deleted!");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateFile([FromRoute] int id, IFormFile file)
        {
            var model = await media.UpdateFile(id, file);
            if (model == null) return NotFound("File Not Found!");
            return Ok("File updated!");
        }

    }
}
