using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Data;

namespace InocuoGoMetrics.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    var stats = new
                    {
                        message = "✅ Conexión exitosa a Neon PostgreSQL",
                        organizaciones = await _context.Organizaciones.CountAsync(),
                        topicos = await _context.Topicos.CountAsync(),
                        subcategorias = await _context.Subcategorias.CountAsync(),
                        usuariosAdmin = await _context.UsuariosAdmin.CountAsync(),
                        usuariosFinales = await _context.Usuarios.CountAsync(),
                        conversaciones = await _context.Conversaciones.CountAsync(),
                        mensajes = await _context.Mensajes.CountAsync()
                    };

                    return Ok(stats);
                }

                return BadRequest("❌ No se pudo conectar a la base de datos");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "❌ Error al conectar",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }
    }
}