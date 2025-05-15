using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Aplication.ValidationAcount;
using Domain.Interface.EmailService;
using Domain.Interface.Service.user;
using Domain.Interface.Service.login;
using Aplication.Service.login;
using Aplication.Service.user;

using Domain.Interface.Service.Perfil;
using Aplication.Service.Subida;


namespace Aplication.Extension
{
    public static class ExtensionService
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration con)
        {
            services.AddTransient<IServiceUser, UserServices>();
            services.AddTransient<IServiceLogin, ServiceLogin>();
            services.AddTransient<TokenService>();
            services.AddTransient<ValidacionActivarAccount>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMediafile, MediaFilesServices>();
        }
    }
}
