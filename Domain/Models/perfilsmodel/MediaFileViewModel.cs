

    using Microsoft.AspNetCore.Http;

    namespace Domain.Models.perfilsmodel
    {
        public class MediaFileViewModel
        {
            public int? PublicacionId { get; set; }


            public IFormFile File { get; set; }  // El archivo de imagen o video
            public int UserId { get; set; }  // El usuario que sube el archivo
            public string FileType { get; set; }  // Puede ser 'ProfilePhoto', 'Image', 'Video', etc.

            public string FilePath { get; set; }
        }
    }
