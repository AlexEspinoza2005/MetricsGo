using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;
        private readonly EmailService _emailService;

        public AuthController(ApiService apiService, EmailService emailService)
        {
            _apiService = apiService;
            _emailService = emailService;
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

        // POST: Auth/RecuperarPassword
        [HttpPost]
        public async Task<IActionResult> RecuperarPassword(string correoAdm)
        {
            try
            {
                var usuarios = await _apiService.GetAsync<List<UsuarioDto>>("UsuariosAdmin");
                var usuario = usuarios?.FirstOrDefault(u => u.correoAdm == correoAdm);

                if (usuario == null)
                {
                    ViewBag.Success = "Si el correo existe, hemos enviado un enlace.";
                    return View();
                }

                string link = Url.Action("CambiarPassword", "Auth", new { token = correoAdm }, Request.Scheme);

                // ✅ HTML ULTRA-SIMPLE (Gmail no lo bloqueará)
                string mensaje = $@"
<html>
<body>
<h2>InocuoGo - Restablecer Contraseña</h2>
<p>Hola <strong>{usuario.nombreAdm}</strong>,</p>
<p>Haz clic en el siguiente enlace para cambiar tu contraseña:</p>
<p><a href='{link}' style='color: #198754; font-size: 16px;'>Cambiar mi contraseña</a></p>
<p>Si no solicitaste este cambio, ignora este correo.</p>
</body>
</html>";

                bool enviado = await _emailService.EnviarCorreo(correoAdm, "Cambiar Contraseña - InocuoGo", mensaje);

                if (enviado)
                    ViewBag.Success = "¡Enviado! Revisa tu correo para continuar.";
                else
                    ViewBag.Error = "Error al enviar el correo.";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error técnico: {ex.Message}";
                return View();
            }
        }


        // GET: Auth/CambiarPassword
        [HttpGet]
        public IActionResult CambiarPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Token = token;
            return View();
        }

        // POST: Auth/CambiarPassword
        [HttpPost]
        public async Task<IActionResult> CambiarPassword(string correoToken, string nuevaPassword, string confirmPassword)
        {
            if (nuevaPassword != confirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                ViewBag.Token = correoToken;
                return View();
            }

            try
            {
                var usuarios = await _apiService.GetAsync<List<UsuarioDto>>("UsuariosAdmin");
                var usuario = usuarios?.FirstOrDefault(u => u.correoAdm == correoToken);

                if (usuario == null)
                {
                    ViewBag.Error = "Enlace inválido o usuario no encontrado.";
                    return View();
                }

                var datosActualizar = new
                {
                    usuario.nombreAdm,
                    usuario.correoAdm,
                    passAdm = nuevaPassword,
                    usuario.idOrgAdm
                };

                await _apiService.PutAsync($"UsuariosAdmin/{usuario.idAdm}", datosActualizar);

                ViewBag.Success = "Contraseña actualizada. Ya puedes iniciar sesión.";
                ViewBag.Token = null;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error técnico: " + ex.Message;
                ViewBag.Token = correoToken;
                return View();
            }
        }
    }

    // Modelos exclusivos de Auth
    public class LoginResponse
    {
        public string idAdm { get; set; }
        public string nombreAdm { get; set; }
        public string correoAdm { get; set; }
        public string idOrgAdm { get; set; }
    }

    public class UsuarioDto
    {
        public string idAdm { get; set; }
        public string nombreAdm { get; set; }
        public string correoAdm { get; set; }
        public string idOrgAdm { get; set; }
    }
}