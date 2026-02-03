using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.DTOs;
using InocuoGoMetrics.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Where(t => t.ActivoTem)
                .ToListAsync();
        }

        // GET: api/Topicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topico>> GetTopico(int id)
        {
            var topico = await _context.Topicos
                .Include(t => t.Subcategorias)
                .FirstOrDefaultAsync(t => t.IdTem == id);

            if (topico == null)
            {
                return NotFound();
            }

            return topico;
        }

        // POST: api/Topicos
        [HttpPost]
        public async Task<ActionResult<Topico>> PostTopico(Topico topico)
        {
            topico.CreadoTem = DateTime.UtcNow;
            topico.ActivoTem = true;

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopico), new { id = topico.IdTem }, topico);
        }

        // PUT: api/Topicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopico(int id, Topico topico)
        {
            if (id != topico.IdTem)
            {
                return BadRequest();
            }

            _context.Entry(topico).State = EntityState.Modified;

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
        public async Task<IActionResult> DeleteTopico(int id)
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

        private bool TopicoExists(int id)
        {
            return _context.Topicos.Any(e => e.IdTem == id);
        }
    }
}
