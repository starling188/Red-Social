

using Microsoft.AspNetCore.Http;

namespace Aplication.Interface.Cloudinary
{
    public interface IClaudinaryUpload
    {

        Task<CloudinaryDotNet.Actions.ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<CloudinaryDotNet.Actions.VideoUploadResult> UploadVideoAsync(IFormFile file);

    }
}
