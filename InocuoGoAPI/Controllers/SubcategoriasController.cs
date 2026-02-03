using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.Models;
using InocuoGoMetrics.API.DTOs;

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
        public async Task<ActionResult<IEnumerable<Subcategoria>>> GetSubcategoriasPorTopico(long topicoId)  // ✅ CAMBIO: int → long
        {
            return await _context.Subcategorias
                .Where(s => s.IdTemSub == topicoId && s.ActivoSub)
                .ToListAsync();
        }

        // GET: api/Subcategorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategoria>> GetSubcategoria(long id)  // ✅ CAMBIO: int → long
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
        public async Task<ActionResult<Subcategoria>> PostSubcategoria(SubcategoriaCreateDto dto)  // ✅ CAMBIO: usa DTO
        {
            var subcategoria = new Subcategoria
            {
                NombreSub = dto.NombreSub,
                DescriSub = dto.DescriSub,
                IdTemSub = dto.IdTemSub,
                ActivoSub = true,
                CreadoSub = DateTime.UtcNow
            };

            _context.Subcategorias.Add(subcategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubcategoria), new { id = subcategoria.IdSub }, subcategoria);
        }

        // PUT: api/Subcategorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategoria(long id, SubcategoriaUpdateDto dto)  // ✅ CAMBIO: int → long, usa DTO
        {
            var subcategoria = await _context.Subcategorias.FindAsync(id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            subcategoria.NombreSub = dto.NombreSub;
            subcategoria.DescriSub = dto.DescriSub;
            subcategoria.IdTemSub = dto.IdTemSub;
            subcategoria.ActivoSub = dto.ActivoSub;

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
        public async Task<IActionResult> DeleteSubcategoria(long id)  // ✅ CAMBIO: int → long
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

        private bool SubcategoriaExists(long id)  // ✅ CAMBIO: int → long
        {
            return _context.Subcategorias.Any(e => e.IdSub == id);
        }
    }
}