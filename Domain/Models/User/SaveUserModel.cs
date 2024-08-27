

using System.ComponentModel.DataAnnotations;

namespace Domain.Models.User
{
    public class SaveUserModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Correo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
