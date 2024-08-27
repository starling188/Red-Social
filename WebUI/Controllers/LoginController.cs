
using Aplication.Service;
using Domain.Interface.Service;
using Domain.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebRed.Models;

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

      

        public IActionResult Loguear() { return View(); }



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
        public async Task<IActionResult> Sign(InicioModel model)
        {
            if (ModelState.IsValid)
            {

                var usuario = await _log.InicioSession(model);

                if (usuario != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim("Correo", usuario.Correo)

                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                }

               

            } return View(model);
        }
    }
}
