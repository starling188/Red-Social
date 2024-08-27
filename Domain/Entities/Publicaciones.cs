

using Domain.Core;

namespace Domain.Entities
{
    public class Publicaciones : BaseEntity
    {
        public int UsuarioID { get; set; }
        public string Contenido { get; set; }
        public string Imagen { get; set; }
        public string EnlaceVideo { get; set; }

        public virtual User Usuario { get; set; }
        public virtual ICollection<Comentarios> Comentarios { get; set; }

    }

}
