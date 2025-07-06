using System.ComponentModel.DataAnnotations;

namespace Aplication.Dtos.Login;

public class InicioModel
{
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Correo {  get; set; }

    public string Contrasena { get; set; }
}
