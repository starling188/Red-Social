
namespace Aplication.Service.upload
{
    public class MediaFileViewModel
    {
        public IFormFile File { get; set; } // El archivo que se subirá
        public string FileType { get; set; } // Tipo de archivo (Foto de perfil, Imagen, Video)
        public int UploadedByUserId { get; set; } // Usuario que sube el archivo
    }
}
