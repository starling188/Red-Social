
using Domain.Core;

namespace Domain.Entities
{
    public class Comentarios : BaseEntity
    {
        public int PublicacionID { get; set; }
        public int UsuarioID { get; set; }
        public string Contenido { get; set; }

        public virtual Publicaciones Publicacion { get; set; }
        public virtual User Usuario { get; set; }
    }
}
