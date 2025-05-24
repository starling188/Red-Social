using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Domain.Interface.Service.Perfil;
using Domain.Models.perfilsmodel;
using Domain.Interface.Service.user;
using Domain.Models.User;






namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IMediafile _mediafile;
        private readonly IServiceUser _user;

        public HomeController(IMediafile med , IServiceUser us)
        {
            _mediafile = med;
            _user = us;
        }

        public async Task<IActionResult> Index()
        {
            await Getphoto();
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

        //for index extend
        private async Task Getphoto()
        {
            var username = User.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {

                var usuario = await _user.GetProfDataUser(username);
                ViewBag.PhotoPerfil = usuario.FotoPerfil;
            }
        }


        //not remove this


        [HttpGet]
        public async Task<IActionResult> UserProfile(string username)
        {

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Obtener los datos del perfil del usuario
                var perfil = await _user.GetProfDataUser(username);

                // Cargar la foto de perfil para la barra superior
                

                return View(perfil);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> SearchByUserNames(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Json(new List<SearchUserDto>()); // <-- CORREGIDO

            try
            {
                var results = await _user.SearchUserByUserName(username);
                return Json(results); // Esto será consumido por JavaScript
            }
            catch
            {
                return Json(new List<SearchUserDto>()); // <-- CORREGIDO
            }
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {

                return RedirectToAction("Loguear", "Login");
            }

            try
            {
                var perfil = await _user.GetProfDataUser(username);
                return View(perfil);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

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