

namespace Aplication.Interface.EmailServices
{
    public interface IEmailService
    {
        Task EnviarCorreoActivacionAsync(string destinatario, string token);
    }
}
