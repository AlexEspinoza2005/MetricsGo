using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InocuoGoMetrics.Filters
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controlador = context.RouteData.Values["controller"]?.ToString();

            if (controlador == "Auth")
            {
                base.OnActionExecuting(context);
                return;
            }

            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("UsuarioId")))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}