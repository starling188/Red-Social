

namespace Domain.Interface.EmailService
{
    public interface IEmailService
    {
        Task EnviarCorreoActivacionAsync(string destinatario, string token);
    }
}
