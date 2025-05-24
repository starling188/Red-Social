using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Extension;
using Aplication.Extension;
using Aplication.Dtos;

using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SocialRedContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));
});


builder.Services.AddApplicationServices(builder.Configuration);



// Registrar los repositorios de la capa de infraestructura
builder.Services.ExtensionRepository(builder.Configuration);

builder.Services.AddSingleton(Automaper.Configure());

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Loguear"; // Ruta de login
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
    });




var app = builder.Build();

app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication(); // Debe estar antes de UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Loguear}/{id?}");

app.Run();
