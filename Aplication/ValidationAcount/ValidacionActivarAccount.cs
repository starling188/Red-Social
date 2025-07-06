

using Aplication.Interface.ValidacionService;
using Domain.Entities;
using Domain.Interface.Repositories;

namespace Aplication.ValidationAcount
{
    public class ValidacionActivarAccount : IValidationAccount
    {
        private readonly IRepositoryUser _UserRepo;

        public ValidacionActivarAccount(IRepositoryUser user)
        {
            _UserRepo = user;
        }
        public async Task<User?> ActivarCuentaAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token inválido
                }

                var usuario = await _UserRepo.GetByCondition(u => u.ActivacionToken == token);

                if (usuario == null || usuario.EstadoActivacion)
                {
                    return null; // Usuario no encontrado o ya activado
                }

                DateTime createdDateUtc = TimeZoneInfo.ConvertTimeToUtc(usuario.CreatedDate);
                DateTime expirationTime = createdDateUtc.AddMinutes(5);

                if (DateTime.UtcNow > expirationTime)
                {
                    return null; // Token expirado
                }

                usuario.EstadoActivacion = true;
                usuario.ActivacionToken = null;
                await _UserRepo.Update(usuario); // Actualiza el usuario en la base de datos
                await _UserRepo.Save(); // Guarda los cambios

                return usuario; // Devuelve el usuario activado
            }
            catch (Exception ex)
            {
                // Log exception or handle it
                Console.WriteLine($"Error al activar cuenta: {ex.Message}");
                return null;
            }
        }

    }
}
