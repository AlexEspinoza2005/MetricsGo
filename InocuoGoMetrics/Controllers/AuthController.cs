using InocuoGoMetrics.Services;
using InocuoGoMetrics.DTOs; 
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;
        private readonly SendGridEmailService _emailService; 

        public AuthController(ApiService apiService, SendGridEmailService emailService) 
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

                string mensaje = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
    <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
        <div style='text-align: center; margin-bottom: 30px;'>
            <h1 style='color: #198754; margin: 0;'>🍃 InocuoGo</h1>
        </div>
        
        <h2 style='color: #333; margin-bottom: 20px;'>Restablecer Contraseña</h2>
        
        <p style='color: #666; font-size: 16px; line-height: 1.5;'>
            Hola <strong>{usuario.nombreAdm}</strong>,
        </p>
        
        <p style='color: #666; font-size: 16px; line-height: 1.5;'>
            Recibimos una solicitud para restablecer tu contraseña. Haz clic en el botón de abajo para crear una nueva contraseña:
        </p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{link}' style='display: inline-block; background-color: #198754; color: white; padding: 15px 40px; text-decoration: none; border-radius: 5px; font-weight: bold; font-size: 16px;'>
                CAMBIAR MI CONTRASEÑA
            </a>
        </div>
        
        <p style='color: #999; font-size: 14px; margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee;'>
            Si no solicitaste este cambio, puedes ignorar este correo.
        </p>
        
        <p style='color: #999; font-size: 12px; margin-top: 10px;'>
            O copia este enlace en tu navegador:<br>
            <a href='{link}' style='color: #198754; word-break: break-all;'>{link}</a>
        </p>
    </div>
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
}