
using Domain.Entities;

namespace Aplication.Interface.ValidacionService
{
    public interface IValidationAccount
    {
        Task<User?> ActivarCuentaAsync(string token);
    }
}
