using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class UserPhotoActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Transferir los datos del middleware al ViewBag
            if (context.HttpContext.Items.ContainsKey("UserPhoto"))
            {
                if (context.Controller is Controller controller)
                {
                    controller.ViewBag.PhotoPerfil = context.HttpContext.Items["UserPhoto"]?.ToString();
                }
            }

            if (context.HttpContext.Items.ContainsKey("UserData"))
            {
                if (context.Controller is Controller controller)
                {
                    controller.ViewBag.UserData = context.HttpContext.Items["UserData"];
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No necesitamos hacer nada después de la ejecución
        }
    }
}
