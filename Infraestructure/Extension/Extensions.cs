


using Domain.Interface.Repositories;

using Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Extension
{
    public static class Extensions
    {
        public static void ExtensionRepository(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IRepositoryAmistades, AmistadesRepository>();
            services.AddTransient<IRepositoryComentarios, ComentariosRepository>();
            services.AddTransient<IRepositoryUser, UserRepository>();
            services.AddTransient<IRepositoryPublicaciones, PublicacionRepository>();
            services.AddTransient<IRepositoryLogin, LoginRepositorio>();
           
            services.AddTransient<IRepositoryMediaFile, MediaFileRepository>();

    
        }
    }
}
