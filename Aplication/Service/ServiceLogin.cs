
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Service;
using WebRed.Models;

namespace Aplication.Service
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly IRepositoryLogin _log;
        private readonly IMapper _map;

        public ServiceLogin(IRepositoryLogin log, IMapper map)
        {
            _map = map;
            _log = log;
            
        }

        public async Task<User> InicioSession(InicioModel model)
        {
            var user = _map.Map<User>(model);

            return await _log.AutenticarUsuario(user.Correo, user.Password);
        }
    }
}
