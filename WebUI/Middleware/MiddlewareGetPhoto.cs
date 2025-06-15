
using Domain.Interface.Service.user;

namespace WebUI.Middleware
{
    public class MiddlewareGetPhoto
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public MiddlewareGetPhoto(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verificar si el usuario está autenticado
            if (context.User.Identity.IsAuthenticated)
            {
                try
                {
                    // Crear un scope para obtener los servicios
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IServiceUser>();
                        var username = context.User.Identity.Name;

                        if (!string.IsNullOrEmpty(username))
                        {
                            // Obtener los datos del perfil (pero solo usaremos la foto)
                            var perfilData = await userService.GetProfDataUser(username);

                            // Almacenar solo la foto en el contexto
                            context.Items["UserPhoto"] = perfilData?.FotoPerfil;

                            // También almacenar datos básicos del usuario
                            context.Items["UserData"] = new
                            {
                                FotoPerfil = perfilData?.FotoPerfil,
                                NombreUsuario = perfilData?.UserName ?? username,
                                NombreCompleto = !string.IsNullOrEmpty(perfilData?.Nombre)
                                    ? $"{perfilData.Nombre} {perfilData.Apellido}".Trim()
                                    : username
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, establecer valores por defecto
                    Console.WriteLine($"Error en MiddlewareGetPhoto: {ex.Message}");

                    context.Items["UserPhoto"] = null;
                    context.Items["UserData"] = new
                    {
                        FotoPerfil = (string)null,
                        NombreUsuario = context.User.Identity.Name,
                        NombreCompleto = context.User.Identity.Name
                    };
                }
            }

            // Continuar con el siguiente middleware
            await _next(context);
        }
    }
}
