using Microsoft.AspNetCore.Mvc;
using InocuoGoMetrics.Services;

namespace InocuoGoMetrics.Controllers
{
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
            var usuarios = await _apiService.GetAsync<List<dynamic>>("UsuariosAdmin");
            return View(usuarios ?? new List<dynamic>());
        }

        // GET: UsuariosAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuariosAdmin/Create
        [HttpPost]
        public async Task<IActionResult> Create(dynamic usuario)
        {
            await _apiService.PostAsync<dynamic>("UsuariosAdmin", usuario);
            return RedirectToAction(nameof(Index));
        }

        // GET: UsuariosAdmin/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var usuario = await _apiService.GetAsync<dynamic>($"UsuariosAdmin/{id}");
            return View(usuario);
        }

        // POST: UsuariosAdmin/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, dynamic usuario)
        {
            await _apiService.PutAsync($"UsuariosAdmin/{id}", usuario);
            return RedirectToAction(nameof(Index));
        }

        // POST: UsuariosAdmin/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _apiService.DeleteAsync($"UsuariosAdmin/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}