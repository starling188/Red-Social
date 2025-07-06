using Domain.Entities;
using Aplication.Dtos.users;

namespace Aplication.Interface.user
{
    public interface IServiceUser : IGenericService<SaveUserModel, User>
    {
        Task<List<SearchUserDto>> SearchUserByUserName(string username);

        Task<PerfilUsuarioDto> GetProfDataUser(string username);
    }
}