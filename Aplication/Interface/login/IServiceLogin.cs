using Domain.Entities;
using Aplication.Dtos.Login;


namespace Aplication.Interface.login
{
    public interface IServiceLogin
    {
        Task<User> InicioSession(InicioModel model);
        Task<bool> RestablecerPassword(string username, string newpassword);
    }
}
