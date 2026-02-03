using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            // Validación básica temporal
            if (correo == "admin@mail.com" && password == "123")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Logout()
        {
            // Aquí irá lógica de cerrar sesión
            return RedirectToAction("Login");
        }

        public IActionResult RecuperarPassword()
        {
            return View();
        }

        public IActionResult CambiarPassword()
        {
            return View();
        }
    }
}