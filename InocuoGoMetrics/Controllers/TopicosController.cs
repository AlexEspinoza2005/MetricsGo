using InocuoGoMetrics.Models;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class TopicosController : Controller
    {
        // Lista estática en memoria (simula la BD)
        private static List<Topico> topicos = new List<Topico>
        {
            new Topico { Id = 1, Titulo = "Higiene Personal", Descripcion = "Normas para el manipulador de alimentos", Icono = "fa-hands-wash" },
            new Topico { Id = 2, Titulo = "Limpieza de Equipos", Descripcion = "Procedimientos de sanitización", Icono = "fa-spray-can" },
            new Topico { Id = 3, Titulo = "Control de Temperatura", Descripcion = "Monitoreo térmico de alimentos", Icono = "fa-temperature-high" }
        };

        // GET: Topicos
        public IActionResult Index()
        {
            return View(topicos);
        }

        // GET: Topicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Topico topico)
        {
            if (ModelState.IsValid)
            {
                topico.Id = topicos.Any() ? topicos.Max(t => t.Id) + 1 : 1;
                topicos.Add(topico);
                return RedirectToAction(nameof(Index));
            }
            return View(topico);
        }

        // GET: Topicos/Edit/5
        public IActionResult Edit(int id)
        {
            var topico = topicos.FirstOrDefault(t => t.Id == id);
            if (topico == null)
                return NotFound();

            return View(topico);
        }

        // POST: Topicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Topico topico)
        {
            if (id != topico.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existente = topicos.FirstOrDefault(t => t.Id == id);
                if (existente != null)
                {
                    existente.Titulo = topico.Titulo;
                    existente.Descripcion = topico.Descripcion;
                    existente.Icono = topico.Icono;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topico);
        }

        // POST: Topicos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var topico = topicos.FirstOrDefault(t => t.Id == id);
            if (topico != null)
            {
                topicos.Remove(topico);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}