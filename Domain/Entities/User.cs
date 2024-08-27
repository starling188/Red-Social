

using Domain.Core;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EstadoActivacion { get; set; } = false;
        public string? ActivacionToken { get; set; }



        public virtual ICollection<Publicaciones> Publicaciones { get; set; }
        public virtual ICollection<Comentarios> Comentarios { get; set; }
        public virtual ICollection<Amistades> Amistades { get; set; }



    }
}
