
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Models.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Aplication.CloudinaryService
{
    public class CloudinaryServices
    {
        private readonly Cloudinary _cloudinary;


        public CloudinaryServices(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);


            _cloudinary = new Cloudinary(acc);
        }
        

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, stream),
                Folder = "socialred/images"
            };
            return await _cloudinary.UploadAsync(uploadParams);
        }

        public async Task<VideoUploadResult> UploadVideoAsync(IFormFile file)
        {

            using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(file.Name, stream),
                Folder = "socialred/videos"
            };
            return await _cloudinary.UploadAsync(uploadParams);
        }
    }
}
