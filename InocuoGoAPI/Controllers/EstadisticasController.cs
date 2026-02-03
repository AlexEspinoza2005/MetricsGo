using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.DTOs;

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
        public async Task<ActionResult<DashboardResponse>> GetDashboard()
        {
            var totalConversaciones = await _context.Conversaciones.CountAsync();
            var usuariosUnicos = await _context.Usuarios.CountAsync(u => u.ActivoUsu);

            // Temas recurrentes (top 5)
            var temasRecurrentes = await _context.Clasificaciones
                .Include(c => c.Subcategoria)
                .ThenInclude(s => s.Topico)
                .Where(c => c.Subcategoria != null && c.Subcategoria.Topico != null)
                .GroupBy(c => c.Subcategoria.Topico.NombreTem)
                .Select(g => new { Tema = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToDictionaryAsync(x => x.Tema, x => x.Cantidad);

            return Ok(new DashboardResponse
            {
                TotalConversaciones = totalConversaciones,
                UsuariosUnicos = usuariosUnicos,
                TemasRecurrentes = temasRecurrentes
            });
        }

        // GET: api/Estadisticas/conversaciones
        [HttpGet("conversaciones")]
        public async Task<ActionResult<IEnumerable<ConversacionResumenDto>>> GetConversaciones()
        {
            var conversaciones = await _context.Conversaciones
                .Include(c => c.Usuario)
                .Include(c => c.Chatbot)
                .OrderByDescending(c => c.Iniciocon)
                .Take(50)
                .Select(c => new ConversacionResumenDto
                {
                    IdCon = c.IdCon,
                    Usuario = c.Usuario.NombreUsu ?? "Anónimo",
                    Chatbot = c.Chatbot.NombreBot,
                    EstadoCon = c.EstadoCon,
                    Iniciocon = c.Iniciocon,
                    ActualizaCon = c.ActualizaCon
                })
                .ToListAsync();

            return Ok(conversaciones);
        }

        // GET: api/Estadisticas/mensajes/conversacion/{conversacionId}
        [HttpGet("mensajes/conversacion/{conversacionId}")]
        public async Task<ActionResult<IEnumerable<MensajeResumenDto>>> GetMensajesPorConversacion(Guid conversacionId)  // ✅ CAMBIO: int → Guid
        {
            var mensajes = await _context.Mensajes
                .Where(m => m.IdConMen == conversacionId)
                .OrderBy(m => m.CreadoMen)
                .Select(m => new MensajeResumenDto
                {
                    IdMen = m.IdMen,
                    TipoMen = m.TipoMen,
                    CuerpoMen = m.CuerpoMen ?? string.Empty,
                    CreadoMen = m.CreadoMen
                })
                .ToListAsync();

            return Ok(mensajes);
        }
    }
}