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
            // CAMBIO: Usamos List<TopicoResponse> en lugar de List<dynamic>
            var topicos = await _apiService.GetAsync<List<TopicoResponse>>("Topicos");
            return View(topicos ?? new List<TopicoResponse>());
        }

        // GET: Topicos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicoResponse topico)
        {
            await _apiService.PostAsync<TopicoResponse>("Topicos", topico);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var topico = await _apiService.GetAsync<TopicoResponse>($"Topicos/{id}");
            return View(topico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, TopicoResponse topico)
        {
            await _apiService.PutAsync($"Topicos/{id}", topico);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await _apiService.DeleteAsync($"Topicos/{id}");
            return RedirectToAction(nameof(Index));
        }

    }

    public class TopicoResponse
    {
        public long idTem { get; set; }
        public string nombreTem { get; set; }
        public string descriTem { get; set; }
        public DateTime creadoTem { get; set; }
        public string idOrgTem { get; set; } 
        public bool activoTem { get; set; }   
    }
}