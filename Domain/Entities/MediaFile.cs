    using Domain.Core;

    namespace Domain.Entities
    {
        public class MediaFile : BaseEntity
        {
            public string FileName { get; set; } // Nombre del archivo
            public string FilePath { get; set; } // Ruta del archivo
            public string ContentType { get; set; } // Tipo de contenido (image/png, video/mp4)
            public DateTime UploadedAt { get; set; } // Fecha de subida
            public int UploadedByUserId { get; set; } // Usuario que subió el archivo
            public string FileType { get; set; } // ProfilePhoto, Image, Video



            public int? PublicacionId { get; set; }
            public Publicaciones Publicacion { get; set; }
            public User User { get; set; } // Propiedad de navegación para EF

        }
    }
