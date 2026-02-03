using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace InocuoGoMetrics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Estadisticas/dashboard
        [HttpGet("dashboard")]
        public async Task<ActionResult> GetDashboard()
        {
            var totalConversaciones = await _context.Conversaciones.CountAsync();
            var usuariosUnicos = await _context.Usuarios.CountAsync(u => u.ActivoUsu);

            // Temas recurrentes (top 5)
            var temasRecurrentes = await _context.Clasificaciones
                .Include(c => c.Subcategoria)
                .ThenInclude(s => s.Topico)
                .GroupBy(c => c.Subcategoria.Topico.NombreTem)
                .Select(g => new { Tema = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToDictionaryAsync(x => x.Tema, x => x.Cantidad);

            return Ok(new
            {
                totalConversaciones,
                usuariosUnicos,
                temasRecurrentes
            });
        }

        // GET: api/Estadisticas/conversaciones
        [HttpGet("conversaciones")]
        public async Task<ActionResult> GetConversaciones()
        {
            var conversaciones = await _context.Conversaciones
                .Include(c => c.Usuario)
                .Include(c => c.Chatbot)
                .OrderByDescending(c => c.Iniciocon)
                .Take(50)
                .Select(c => new
                {
                    c.IdCon,
                    Usuario = c.Usuario.NombreUsu,
                    Chatbot = c.Chatbot.NombreBot,
                    c.EstadoCon,
                    c.Iniciocon,
                    c.ActualizaCon
                })
                .ToListAsync();

            return Ok(conversaciones);
        }

        // GET: api/Estadisticas/mensajes/conversacion/5
        [HttpGet("mensajes/conversacion/{conversacionId}")]
        public async Task<ActionResult> GetMensajesPorConversacion(int conversacionId)
        {
            var mensajes = await _context.Mensajes
                .Where(m => m.IdConMen == conversacionId)
                .OrderBy(m => m.CreadoMen)
                .Select(m => new
                {
                    m.IdMen,
                    m.TipoMen,
                    m.CuerpoMen,
                    m.CreadoMen
                })
                .ToListAsync();

            return Ok(mensajes);
        }
    }
}