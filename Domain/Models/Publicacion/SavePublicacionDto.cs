

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Publicacion
{
    public class SavePublicacionDto
    {
        [Required(ErrorMessage = "El contenido es requerido")]
        [StringLength(500, ErrorMessage = "El contenido no puede exceder 500 caracteres")]
        public string Contenido { get; set; }

        // CAMBIO IMPORTANTE: Usar List<IFormFile> en lugar de IFormFileCollection
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();

        public int UsuarioId { get; set; }
    }
}
