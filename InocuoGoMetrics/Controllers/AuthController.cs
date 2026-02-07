using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            try
            {
                var loginData = new { login, password };

                var response = await _apiService.PostAsync<LoginResponse>("UsuariosAdmin/login", loginData);

                if (response != null)
                {
                    HttpContext.Session.SetString("UsuarioId", response.idAdm.ToString());
                    HttpContext.Session.SetString("UsuarioNombre", response.nombreAdm);
                    HttpContext.Session.SetString("UsuarioCorreo", response.correoAdm);
                    HttpContext.Session.SetString("OrgId", response.idOrgAdm.ToString());

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401") || ex.Message.Contains("Unauthorized"))
                {
                    ViewBag.Error = "Credenciales incorrectas. Por favor verifica tu correo y contraseña.";
                }
                else
                {
                    ViewBag.Error = "No se pudo conectar con el servidor. Intente más tarde.";
 
                }

                return View();
            }
        }

        // GET: Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Auth/RecuperarPassword
        public IActionResult RecuperarPassword()
        {
            return View();
        }

        // GET: Auth/CambiarPassword
        public IActionResult CambiarPassword()
        {
            return View();
        }
    }
    public class LoginResponse
    {
        public string idAdm { get; set; }
        public string nombreAdm { get; set; }
        public string correoAdm { get; set; }
        public string idOrgAdm { get; set; }
    }
}