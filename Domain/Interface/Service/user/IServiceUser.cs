using Domain.Entities;
using Domain.Models.User;

namespace Domain.Interface.Service.user
{
    public interface IServiceUser : IGenericService<SaveUserModel, User>
    {

    }
}