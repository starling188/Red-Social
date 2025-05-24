using Aplication.Core;
using Aplication.ValidationAcount;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.EmailService;
using Domain.Interface.Repositories;
using Domain.Interface.Service.user;
using Domain.Models.User;

namespace Aplication.Service.user
{
    public class UserServices : GenericService<SaveUserModel, User>, IServiceUser
    {
        private readonly IRepositoryUser _UserRepo;
        private readonly IMapper _map;
        private readonly IRepositoryLogin _log;
        private readonly TokenService _tokenSer;
        private readonly IEmailService _emailService;


        public UserServices(IRepositoryUser use, IMapper map, IRepositoryLogin log, TokenService token, IEmailService emailSer) : base(use, map)
        {
            _log = log;
            _map = map;
            _UserRepo = use;
            _emailService = emailSer;
            _tokenSer = token;
        }

        public override async Task Add(SaveUserModel model)
        {
            // Validaciones específicas para User
            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                throw new ArgumentException("El nombre no puede ser vacío.");
            }

            if (string.IsNullOrWhiteSpace(model.Correo))
            {
                throw new ArgumentException("El correo no puede ser vacío.");
            }

            var existuserName = await _UserRepo.GetByUsernameAsync(model.UserName);
            if (existuserName != null) { throw new ArgumentException("El nombre de usuario ya existe. Por favor elige otro nombre."); }


            var entity = _map.Map<User>(model);
            entity.CreatedBy = "Sistema";
            entity.Password = await _log.HashearContraseñaAsync(model.Password);

            //generar token
            var tok = _tokenSer.GeneradorToken();
            entity.ActivacionToken = tok;

            //envio de correo
            await _emailService.EnviarCorreoActivacionAsync(entity.Correo, tok);


            // Llamar al método Add del repositorio
            await _UserRepo.Add(entity);
        }


        public async Task<PerfilUsuarioDto> GetProfDataUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.");
            }

            var user = await _UserRepo.GetProfileWithDataAsync(username);

            if (user == null)
            {
                throw new ArgumentException("No se encontró el perfil del usuario.");

            }

            var perfil = _map.Map<PerfilUsuarioDto>(user);
            return perfil;
        }


        public async Task<List<SearchUserDto>> SearchUserByUserName(string username)
        {
            var user = await _UserRepo.SearchUsersByUserNameAsync(username);

           return _map.Map<List<SearchUserDto>>(user);
        }
        

        public override async Task Save()
        {
            
            await _UserRepo.Save();

        }


    }
}
