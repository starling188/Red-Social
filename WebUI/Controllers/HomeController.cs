using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Domain.Interface.Service.Perfil;
using Domain.Models.perfilsmodel;
using Domain.Interface.Service.user;
using Domain.Models.User;
using Domain.Interface.Service.Publication;
using Domain.Models.Publicacion;






namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IMediafile _mediafile;
        private readonly IServiceUser _user;
        private readonly IServicePublicaciones _publicaciones;


        public HomeController(IMediafile med , IServiceUser us, IServicePublicaciones pub)
        {
            _mediafile = med;
            _user = us;
            _publicaciones = pub;
        }

        public async Task<IActionResult> Index()
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

        public IActionResult CrearPublicacion()
        {
            return View();
        }


       


        //not remove this

        [HttpPost]
        public async Task<IActionResult> CrearPublicacion(SavePublicacionDto dto)
        {

            // DEBUG: Agregar logs para verificar qué está llegando
            Console.WriteLine($"Contenido: {dto?.Contenido}");
            Console.WriteLine($"UsuarioId: {dto?.UsuarioId}");
            Console.WriteLine($"Archivos Count: {dto?.Archivos?.Count ?? 0}");


            // Debug adicional: verificar Request.Form.Files
            Console.WriteLine($"Request.Form.Files Count: {Request.Form.Files.Count}");
            foreach (var file in Request.Form.Files)
            {
                Console.WriteLine($"Archivo: {file.Name} - {file.FileName} - Tamaño: {file.Length}");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Por favor, completa todos los campos requeridos correctamente.";
                return View(dto);
            }


            try
            {
                await _publicaciones.AddpublicationWithFile(dto);
                TempData["SuccessMessage"] = "¡Tu publicación se ha compartido exitosamente!";
                return RedirectToAction("Index", "Home");

            }
            catch(Exception ex) 
            {
                TempData["ErrorMessage"] = "Hubo un error al publicar. Por favor, inténtalo de nuevo.";
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        //cuando buscamos usuarios este metodo traer su info en lo coloca en una vista generica 
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
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Index");
            }
        }

        //el buscar usuarios
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



        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Loguear", "Login");
        }

      
       

    }
}