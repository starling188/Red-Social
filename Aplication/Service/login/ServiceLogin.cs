using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Service.login;
using Domain.Models;


namespace Aplication.Service.login
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

        public async Task<bool> RestablecerPassword(string username, string newpassword)
        {
            return await _log.ReestablecerPassword(username, newpassword);
        }

        public async Task<User> InicioSession(InicioModel model)
        {
            var user = _map.Map<User>(model);

            return await _log.AutenticarUsuario(user.Correo, user.Password);
        }
    }
}
