using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Domain.Interface.Service.Perfil;
using Domain.Models.perfilsmodel;






namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IMediafile _mediafile;

        public HomeController(IMediafile med)
        {
            _mediafile = med;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SubidaFoto()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> SubidaFoto(MediaFileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _mediafile.UploadMediaFileAsync(model);
                await _mediafile.Save();

                TempData["Message"] = "Archivo cargado exitosamente.";

                // Redirige al Index solo si se subió correctamente
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejo de error: se queda en la misma vista
                ModelState.AddModelError(string.Empty, $"Error al cargar el archivo: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Loguear", "Login");
        }

      
       

    }
}