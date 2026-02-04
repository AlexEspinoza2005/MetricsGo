using Microsoft.AspNetCore.Mvc;
using InocuoGoMetrics.Services;

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
                // Cambiamos Dictionary por LoginResponse
                var response = await _apiService.PostAsync<LoginResponse>("UsuariosAdmin/login", loginData);

                if (response != null)
                {
                    // Ahora el acceso es directo y seguro
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
                ViewBag.Error = "Error al iniciar sesión: " + ex.Message;
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