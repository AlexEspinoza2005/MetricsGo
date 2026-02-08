using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using InocuoGoMetrics.DTOs; 
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    [ValidarSesion]
    public class UsuariosAdminController : Controller
    {
        private readonly ApiService _apiService;

        public UsuariosAdminController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: UsuariosAdmin
        public async Task<IActionResult> Index()
        {
            var usuarios = await _apiService.GetAsync<List<UsuarioAdminResponse>>("UsuariosAdmin");
            return View(usuarios ?? new List<UsuarioAdminResponse>());
        }

        // GET: UsuariosAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioAdminResponse usuario)
        {
            try
            {
                usuario.creadoAdm = DateTime.Now;
                await _apiService.PostAsync<UsuarioAdminResponse>("UsuariosAdmin", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear: " + ex.Message;
                return View(usuario);
            }
        }

        // GET: UsuariosAdmin/Edit/uuid
        public async Task<IActionResult> Edit(string id)
        {
            var usuario = await _apiService.GetAsync<UsuarioAdminResponse>($"UsuariosAdmin/{id}");
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UsuarioAdminResponse usuario)
        {
            try
            {
                await _apiService.PutAsync($"UsuariosAdmin/{id}", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar: " + ex.Message;
                return View(usuario);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _apiService.DeleteAsync($"UsuariosAdmin/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}