using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.Models;
using InocuoGoMetrics.API.DTOs;

namespace InocuoGoMetrics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TopicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Topicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topico>>> GetTopicos()
        {
            return await _context.Topicos
                .Include(t => t.Subcategorias)
                .Include(t => t.Organizacion)
                .Where(t => t.ActivoTem)
                .ToListAsync();
        }

        // GET: api/Topicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topico>> GetTopico(long id)  // ✅ CAMBIO: int → long
        {
            var topico = await _context.Topicos
                .Include(t => t.Subcategorias)
                .Include(t => t.Organizacion)
                .FirstOrDefaultAsync(t => t.IdTem == id);

            if (topico == null)
            {
                return NotFound();
            }

            return topico;
        }

        // POST: api/Topicos
        [HttpPost]
        public async Task<ActionResult<Topico>> PostTopico(TopicoCreateDto dto)  // ✅ CAMBIO: usa DTO
        {
            var topico = new Topico
            {
                NombreTem = dto.NombreTem,
                DescriTem = dto.DescriTem,
                IdOrgTem = dto.IdOrgTem,  // ✅ NUEVO: requiere organización
                ActivoTem = true,
                CreadoTem = DateTime.UtcNow
            };

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopico), new { id = topico.IdTem }, topico);
        }

        // PUT: api/Topicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopico(long id, TopicoUpdateDto dto)  // ✅ CAMBIO: int → long, usa DTO
        {
            var topico = await _context.Topicos.FindAsync(id);
            if (topico == null)
            {
                return NotFound();
            }

            topico.NombreTem = dto.NombreTem;
            topico.DescriTem = dto.DescriTem;
            topico.IdOrgTem = dto.IdOrgTem;
            topico.ActivoTem = dto.ActivoTem;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicoExists(id))
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

        // DELETE: api/Topicos/5 (Soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopico(long id)  // ✅ CAMBIO: int → long
        {
            var topico = await _context.Topicos.FindAsync(id);
            if (topico == null)
            {
                return NotFound();
            }

            // Soft delete
            topico.ActivoTem = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicoExists(long id)  // ✅ CAMBIO: int → long
        {
            return _context.Topicos.Any(e => e.IdTem == id);
        }
    }
}