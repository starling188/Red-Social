
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IRepositoryLogin
    {
        Task<User> AutenticarUsuario(string correo, string contraseña);
        Task<bool> ReestablecerPassword(string username, string newPassword);
        Task<string> HashearContraseñaAsync(string contraseña);

    }
}
