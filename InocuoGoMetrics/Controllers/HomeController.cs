using ClosedXML.Excel;
using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using InocuoGoMetrics.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    [ValidarSesion]

    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _apiService.GetAsync<DashboardViewModel>("Estadisticas/dashboard");
            return View(dashboard ?? new DashboardViewModel());
        }

        // GET: Home/EditarPerfil
        public async Task<IActionResult> EditarPerfil()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Login", "Auth");
            }
            var usuario = await _apiService.GetAsync<dynamic>($"UsuariosAdmin/{usuarioId}");
            return View(usuario);
        }

        // POST: Home/EditarPerfil
        [HttpPost]
        public async Task<IActionResult> EditarPerfil(Guid id, string nombreAdm, string correoAdm, string passwAdm)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Login", "Auth");
            }
            var data = new
            {
                nombreAdm,
                correoAdm,
                passwAdm,
                idOrgAdm = HttpContext.Session.GetString("OrgId")
            };
            await _apiService.PutAsync($"UsuariosAdmin/{usuarioId}", data);
            ViewBag.Success = "Perfil actualizado correctamente";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}