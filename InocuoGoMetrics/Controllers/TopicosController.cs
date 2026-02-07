using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    [ValidarSesion]

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
            var topicos = await _apiService.GetAsync<List<TopicoResponse>>("Topicos");
            return View(topicos ?? new List<TopicoResponse>());
        }

        // GET: Topicos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombreTem, string descriTem)
        {
            var topico = new
            {
                nombreTem = nombreTem,
                descriTem = descriTem,
                idOrgTem = HttpContext.Session.GetString("OrgId"),
                activoTem = true
            };

            await _apiService.PostAsync<TopicoResponse>("Topicos", topico);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var topico = await _apiService.GetAsync<TopicoResponse>($"Topicos/{id}");
            return View(topico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, string nombreTem, string descriTem, string idOrgTem, bool activoTem = false)
        {
            var topicoActual = await _apiService.GetAsync<TopicoResponse>($"Topicos/{id}");

            var topico = new
            {
                nombreTem = nombreTem,
                descriTem = descriTem,
                idOrgTem = idOrgTem,
                activoTem = activoTem,
                creadoTem = topicoActual.creadoTem
            };

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