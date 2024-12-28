using Domain.Interface.Service;
using Aplication.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Aplication.ValidationAcount;
using Domain.Interface.EmailService;

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
        }
    }
}
