using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Domain.Interface.EmailService;
using MailKit.Security;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarCorreoActivacionAsync(string destinatario, string token)
    {
        var settings = _configuration.GetSection("MailKit");
        var host = settings["Host"];
        var port = int.Parse(settings["Port"]);
        var userName = settings["UserName"];
        var password = settings["Password"];
        var enableSsl = bool.Parse(settings["EnableSsl"]);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Red Social", userName));
        message.To.Add(new MailboxAddress("", destinatario));
        message.Subject = "Activa tu cuenta";
        message.Body = new TextPart("html")
        {
            Text = $"<p>Haz clic en el siguiente enlace para activar tu cuenta:</p>" +
                $"<a href='http://localhost:5212/Activacion/ActivarCuenta?token={token}'>Validar cuenta</a>"
        };

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(host, port, SecureSocketOptions.StartTls); // Cambiar a StartTls
                await client.AuthenticateAsync(userName, password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Maneja los errores de manera adecuada
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
