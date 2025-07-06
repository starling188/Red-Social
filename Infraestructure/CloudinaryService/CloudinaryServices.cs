
using Aplication.Dtos.Cloudinary;
using Aplication.Interface.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infraestructure.CloudinaryService
{
    public class CloudinaryServices : IClaudinaryUpload
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
