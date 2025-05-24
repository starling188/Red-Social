

namespace Domain.Models.User
{
    public class PerfilUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string FotoPerfil { get; set; }

        public int CantidadSeguidores { get; set; }
        public int CantidadPublicaciones { get; set; }
        public int CantidadArchivosMultimedia { get; set; }

        public List<string> RutasMultimedia { get; set; } = new List<string>();
    }
}
