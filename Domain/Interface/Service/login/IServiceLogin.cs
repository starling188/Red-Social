using Domain.Entities;
using Domain.Models;

namespace Domain.Interface.Service.login
{
    public interface IServiceLogin
    {
        Task<User> InicioSession(InicioModel model);
        Task<bool> RestablecerPassword(string username, string newpassword);
    }
}
