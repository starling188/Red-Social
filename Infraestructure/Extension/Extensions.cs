


using Aplication.Dtos.Cloudinary;
using Aplication.Interface.Cloudinary;
using CloudinaryDotNet;
using Domain.Interface.Repositories;

using Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Infraestructure.CloudinaryService;
using Aplication.Interface.EmailServices;
using Infraestructure.EmailServices;

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
            services.AddTransient<IEmailService, EmailService>();
            //===================================================================




            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            var settings = config.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            var cloudinary = new CloudinaryDotNet.Cloudinary(account);

            services.AddSingleton(cloudinary);
            services.AddTransient<IClaudinaryUpload, CloudinaryServices>();

        }
    }
}
