using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.DTOs;
using InocuoGoMetrics.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InocuoGoMetrics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioAdmin>>> GetUsuariosAdmin()
        {
            return await _context.UsuariosAdmin
                .Include(u => u.Organizacion)
                .ToListAsync();
        }

        // GET: api/UsuariosAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioAdmin>> GetUsuarioAdmin(int id)
        {
            var usuario = await _context.UsuariosAdmin
                .Include(u => u.Organizacion)
                .FirstOrDefaultAsync(u => u.IdAdm == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/UsuariosAdmin/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.UsuariosAdmin
                .FirstOrDefaultAsync(u => u.LoginAdm == request.Login && u.PassAdm == request.Password);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            return Ok(new
            {
                usuario.IdAdm,
                usuario.NombreAdm,
                usuario.LoginAdm,
                usuario.IdOrgAdm
            });
        }

        // POST: api/UsuariosAdmin
        [HttpPost]
        public async Task<ActionResult<UsuarioAdmin>> PostUsuarioAdmin(UsuarioAdmin usuario)
        {
            usuario.CreadoAdm = DateTime.UtcNow;

            _context.UsuariosAdmin.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioAdmin), new { id = usuario.IdAdm }, usuario);
        }

        // PUT: api/UsuariosAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioAdmin(int id, UsuarioAdmin usuario)
        {
            if (id != usuario.IdAdm)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioAdminExists(id))
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

        // DELETE: api/UsuariosAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioAdmin(int id)
        {
            var usuario = await _context.UsuariosAdmin.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.UsuariosAdmin.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioAdminExists(int id)
        {
            return _context.UsuariosAdmin.Any(e => e.IdAdm == id);
        }
    }

    // DTO para login
    public class LoginRequest
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}