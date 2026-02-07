using InocuoGoMetrics.Filters;
using InocuoGoMetrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    [ValidarSesion]

    public class SubcategoriasController : Controller
    {
        private readonly ApiService _apiService;

        public SubcategoriasController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Subcategorias/GetByTopico/5
        [HttpGet]
        public async Task<IActionResult> GetByTopico(long id)
        {
            var subcategorias = await _apiService.GetAsync<List<SubcategoriaResponse>>($"Subcategorias/topico/{id}");
            return Json(subcategorias ?? new List<SubcategoriaResponse>());
        }

        // POST: Subcategorias/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubcategoriaRequest subcategoria)
        {
            try
            {
                await _apiService.PostAsync<SubcategoriaResponse>("Subcategorias", subcategoria);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: Subcategorias/Edit/5
        [HttpPut("Subcategorias/Edit/{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] SubcategoriaUpdateRequest subcategoria)
        {
            try
            {
                await _apiService.PutAsync($"Subcategorias/{id}", subcategoria);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: Subcategorias/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _apiService.DeleteAsync($"Subcategorias/{id}");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }

    // Modelos
    public class SubcategoriaResponse
    {
        public long idSub { get; set; }
        public string nombreSub { get; set; }
        public string descriSub { get; set; }
        public long idTemSub { get; set; }
        public bool activoSub { get; set; }
        public DateTime creadoSub { get; set; }
    }

    public class SubcategoriaRequest
    {
        public string nombreSub { get; set; }
        public string descriSub { get; set; }
        public long idTemSub { get; set; }
    }

    public class SubcategoriaUpdateRequest
    {
        public string nombreSub { get; set; }
        public string descriSub { get; set; }
        public long idTemSub { get; set; }
        public bool activoSub { get; set; }
    }
}