using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.DTOs;
using InocuoGoMetrics.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InocuoGoMetrics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubcategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Subcategorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategoria>>> GetSubcategorias()
        {
            return await _context.Subcategorias
                .Include(s => s.Topico)
                .Where(s => s.ActivoSub)
                .ToListAsync();
        }

        // GET: api/Subcategorias/topico/5
        [HttpGet("topico/{topicoId}")]
        public async Task<ActionResult<IEnumerable<Subcategoria>>> GetSubcategoriasPorTopico(int topicoId)
        {
            return await _context.Subcategorias
                .Where(s => s.IdTemSub == topicoId && s.ActivoSub)
                .ToListAsync();
        }

        // GET: api/Subcategorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategoria>> GetSubcategoria(int id)
        {
            var subcategoria = await _context.Subcategorias
                .Include(s => s.Topico)
                .FirstOrDefaultAsync(s => s.IdSub == id);

            if (subcategoria == null)
            {
                return NotFound();
            }

            return subcategoria;
        }

        // POST: api/Subcategorias
        [HttpPost]
        public async Task<ActionResult<Subcategoria>> PostSubcategoria(Subcategoria subcategoria)
        {
            subcategoria.CreadoSub = DateTime.UtcNow;
            subcategoria.ActivoSub = true;

            _context.Subcategorias.Add(subcategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubcategoria), new { id = subcategoria.IdSub }, subcategoria);
        }

        // PUT: api/Subcategorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategoria(int id, Subcategoria subcategoria)
        {
            if (id != subcategoria.IdSub)
            {
                return BadRequest();
            }

            _context.Entry(subcategoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Subcategorias/5 (Soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategoria(int id)
        {
            var subcategoria = await _context.Subcategorias.FindAsync(id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            // Soft delete
            subcategoria.ActivoSub = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubcategoriaExists(int id)
        {
            return _context.Subcategorias.Any(e => e.IdSub == id);
        }
    }
}