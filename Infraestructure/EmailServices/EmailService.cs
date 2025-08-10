using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

using MailKit.Security;
using Aplication.Interface.EmailServices;

namespace Infraestructure.EmailServices
{
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
                //Text = $"<p>Haz clic en el siguiente enlace para activar tu cuenta:</p>" +
                //    $"<a href='http://localhost:5212/Activacion/ActivarCuenta?token={token}'>Validar cuenta</a>"

                Text = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                            <div style='max-width: 500px; background-color: #ffffff; padding: 20px; border-radius: 10px;'>
                                <h2 style='color: #333;'>Código de activación</h2>
                                <p>Hola,</p>
                                <p>Usa el siguiente código para activar tu cuenta. Este código expirará en 5 minutos.</p>
                                <div style='font-size: 24px; font-weight: bold; color: #2c3e50; 
                                            background-color: #f0f0f0; padding: 10px; border-radius: 5px;
                                            text-align: center; letter-spacing: 3px;'>{token}</div>
                                <p>Para activar tu cuenta:</p>
                                <ol>
                                    <li>Abre la página de activación de tu cuenta.</li>
                                    <li>Introduce el código mostrado arriba.</li>
                                    <li>Presiona <strong>Activar</strong>.</li>
                                </ol>
                               <p>
                                    <a href='http://localhost:5212/Activacion/activarAccount'
                                       style='display: inline-block; padding: 10px 15px; background-color: #28a745; color: white; 
                                              text-decoration: none; border-radius: 5px;'>Haz click aquí para activar tu cuenta</a>
                                </p>    
                                <p style='margin-top: 20px; font-size: 12px; color: #777;'>Si no solicitaste esta activación, ignora este mensaje.</p>
                            </div>
                        </body>
                    </html>"
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
}