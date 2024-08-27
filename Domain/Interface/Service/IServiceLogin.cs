

using Domain.Entities;
using WebRed.Models;

namespace Domain.Interface.Service
{
    public interface IServiceLogin 
    {
        Task<User> InicioSession( InicioModel model);

    }
}
