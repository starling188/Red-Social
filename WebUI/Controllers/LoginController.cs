

using Domain.Interface.Service;
using Domain.Models;
using Domain.Models.Login;
using Domain.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceUser _userService;
        private readonly IServiceLogin _log;

        public LoginController(IServiceUser use, IServiceLogin log)
        {
            _userService = use;
            _log = log;
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Loguear() {

          
            return View();
        
        }

        
        public IActionResult RestablecerContraseña()
        {
            return View();
        }



        //metodos 

        [HttpPost]
        public async Task<IActionResult> RestablecerContraseña(RestablecerPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Asegúrate de validar la nueva contraseña antes de continuar
                if (string.IsNullOrWhiteSpace(model.NuevaContraseña))
                {
                    ModelState.AddModelError(string.Empty, "La nueva contraseña no puede estar vacía.");
                    return View(model);
                }

                var result = await _log.RestablecerPassword(model.UserName, model.NuevaContraseña);

                if (result)
                {
                    TempData["SuccessMessage"] = "Contraseña reestablecida con éxito";
                    return RedirectToAction("Loguear", "Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "El nombre de usuario no fue encontrado";
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Add(model);
                    await _userService.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Loguear(InicioModel model)
        {
            if (ModelState.IsValid)
            {

                var usuario = await _log.InicioSession(model);

                if (usuario != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.UserName),
                        new Claim("Correo", usuario.Correo)

                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    TempData["SuccessMessage"] = "Inicio de sesión exitoso. ¡Bienvenido!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Correo electrónico o contraseña incorrectos.";

                }
            }
            else
            {
                TempData["WarningMessage"] = "Por favor, corrija los errores en el formulario.";
            }

            return View(model);
        }
    }
}
