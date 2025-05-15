



using Aplication.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Models.perfilsmodel;
using Microsoft.Extensions.Hosting;
using Domain.Interface.Service.Perfil;


namespace Aplication.Service.Subida
{
    public class MediaFilesServices : GenericService<MediaFileViewModel, MediaFile> , IMediafile
    {
        private readonly IMapper _map;
        private readonly IRepositoryMediaFile _repo;
        private readonly IHostEnvironment _environment;

        public MediaFilesServices(IRepositoryMediaFile mediaFileRepository, IMapper mapper, IHostEnvironment environment) : base ( mediaFileRepository, mapper)
        {
            _repo = mediaFileRepository;
            _map = mapper;
            _environment = environment;
        }

        public async Task UploadMediaFileAsync(MediaFileViewModel viewModel)
        {

            if (viewModel.File == null || viewModel.File.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser vacío.");
            }


            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".mp4", ".avi", ".mov" };
            var fileExtension = Path.GetExtension(viewModel.File.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Tipo de archivo no permitido.");
            }

            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads"); // Ruta dentro de wwwroot/uploads
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }// Cambiado a ContentRootPath

            var fileName = Path.GetFileNameWithoutExtension(viewModel.File.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await viewModel.File.CopyToAsync(fileStream);
            }

            var mediaFile = _map.Map<MediaFile>(viewModel);

            mediaFile.FileName = uniqueFileName;
            mediaFile.FilePath = filePath;
            mediaFile.ContentType = viewModel.File.ContentType;
            mediaFile.UploadedAt = DateTime.UtcNow;
            mediaFile.IsActive = true;
            mediaFile.CreatedBy = "Sistema";
            mediaFile.UploadedByUserId = viewModel.UserId;
            
            

            

            await _repo.Add(mediaFile);

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
