using Ecommerce.Dtos.MediaDtos;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IMedia
    {
        public Task<MediaModel> CreateMediaFile(CreateFile file);
        public Task<string> UploadImage(IFormFile file);
        public Task<List<string>> UploadFiles(IFormFileCollection files, string userId);
        public Task<MediaModel?> DeleteFile(int id);
        public Task<MediaModel?> UpdateFile(int id, IFormFile file);
    }
}
