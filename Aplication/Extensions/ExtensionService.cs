using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Aplication.ValidationAcount;

using Aplication.Interface.user;
using Aplication.Interface.login;

using Aplication.Service.login;
using Aplication.Service.user;

using Aplication.Service.Subida;
using Aplication.Service.publicacion;




using Aplication.Interface.Publication;


using Aplication.Interface.Perfil;




namespace Aplication.Extension
{
    public static class ExtensionService
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IServiceUser, UserServices>();
            services.AddTransient<IServiceLogin, ServiceLogin>();
            services.AddTransient<TokenService>();
            services.AddTransient<ValidacionActivarAccount>();
            //services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMediafile, MediaFilesServices>();
            services.AddTransient<IServicePublicaciones, PublicacionesService>();




            //services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));


            //var settings = config.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            //var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            //var cloudinary = new CloudinaryDotNet.Cloudinary(account);

            //services.AddSingleton(cloudinary);
            //services.AddTransient<IClaudinaryUpload, CloudinaryServices>();

        }
    }
}
