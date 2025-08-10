using Aplication.Interface.ValidacionService;


using Microsoft.AspNetCore.Mvc;

public class ActivacionController : Controller
{
    private readonly IValidationAccount _val;

    public ActivacionController(IValidationAccount userService)
    {
        _val = userService;
    }



    [HttpGet]
    public IActionResult activarAccount()
    {
        return View();
    }


    //metodo para activar la cuenta del usuario
    [HttpPost]
    public async Task<IActionResult> ActivarCuenta(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError("","debes ingresar un token");
            return View();

        }

        try
        {
            var usuario = await _val.ActivarCuentaAsync(token);

            if (usuario != null)
            {
                // Activación exitosa
                return View("ActivacionExitoso");
            }
            else
            {
                // Activación fallida
                return View("ActivacionFallida");
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores
            Console.WriteLine($"Error al activar cuenta: {ex.Message}");
            return View("Error");
        }
    }
}
