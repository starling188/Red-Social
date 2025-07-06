using Aplication.Interface.ValidacionService;
using Aplication.ValidationAcount;

using Microsoft.AspNetCore.Mvc;

public class ActivacionController : Controller
{
    private readonly IValidationAccount _val;

    public ActivacionController(IValidationAccount userService)
    {
        _val = userService;
    }

    [HttpGet]
    public async Task<IActionResult> ActivarCuenta(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return View("Error"); 
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
