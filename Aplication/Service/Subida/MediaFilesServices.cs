



using Aplication.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Models.perfilsmodel;
using Microsoft.Extensions.Hosting;
using Domain.Interface.Service.Perfil;
using Aplication.CloudinaryService;


namespace Aplication.Service.Subida
{
    public class MediaFilesServices : GenericService<MediaFileViewModel, MediaFile> , IMediafile
    {
        private readonly IMapper _map;
        private readonly IRepositoryMediaFile _repo;
        private readonly IHostEnvironment _environment;
        private readonly CloudinaryServices _cloudinaryService;

        public MediaFilesServices(IRepositoryMediaFile mediaFileRepository, IMapper mapper, IHostEnvironment environment, CloudinaryServices claud) : base ( mediaFileRepository, mapper)
        {
            _repo = mediaFileRepository;
            _map = mapper;
            _environment = environment;
            _cloudinaryService = claud;
        }

        public async Task UploadMediaFileAsync(MediaFileViewModel viewModel)
        {

            if (viewModel.File == null || viewModel.File.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser vacío.");
            }


            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".mp4", ".avi", ".mov", "mkv" };
            var fileExtension = Path.GetExtension(viewModel.File.FileName).ToLower();


            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Tipo de archivo no permitido.");
            }

            string fileType = viewModel.File.ContentType.StartsWith("image") ? "image" :
                      viewModel.File.ContentType.StartsWith("video") ? "video" : "other";

            string url = null;

            if (fileType == "image")
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(viewModel.File);
                url = uploadResult.SecureUrl.ToString();
            }
            else if (fileType == "video")
            {
                var uploadResult = await _cloudinaryService.UploadVideoAsync(viewModel.File);
                url = uploadResult.SecureUrl.ToString();
            }
            else
            {
                throw new Exception("Tipo de archivo no soportado por Cloudinary.");
            }

            var mediafile = _map.Map<MediaFile>(viewModel);
            mediafile.FilePath = url;
            


            await _repo.Add(mediafile);

            
        }

        public async Task Save()
        {
            try
            {
                await _repo.Save();
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o manejarlo según tu necesidad
                Console.WriteLine($"Error al guardar: {ex.Message}");
                throw; // Relanza la excepción si necesitas que se propague
            }
        }

    }
}
