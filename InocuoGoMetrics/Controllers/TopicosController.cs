using Microsoft.AspNetCore.Mvc;
using InocuoGoMetrics.Services;

namespace InocuoGoMetrics.Controllers
{
    public class TopicosController : Controller
    {
        private readonly ApiService _apiService;

        public TopicosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Topicos
        public async Task<IActionResult> Index()
        {
            var topicos = await _apiService.GetAsync<List<dynamic>>("Topicos");
            return View(topicos ?? new List<dynamic>());
        }

        // GET: Topicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topicos/Create
        [HttpPost]
        public async Task<IActionResult> Create(dynamic topico)
        {
            await _apiService.PostAsync<dynamic>("Topicos", topico);
            return RedirectToAction(nameof(Index));
        }

        // GET: Topicos/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var topico = await _apiService.GetAsync<dynamic>($"Topicos/{id}");
            return View(topico);
        }

        // POST: Topicos/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(long id, dynamic topico)
        {
            await _apiService.PutAsync($"Topicos/{id}", topico);
            return RedirectToAction(nameof(Index));
        }

        // POST: Topicos/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await _apiService.DeleteAsync($"Topicos/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}