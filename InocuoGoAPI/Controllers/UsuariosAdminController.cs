using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Data;
using InocuoGoMetrics.API.Models;
using InocuoGoMetrics.API.DTOs;

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
        public async Task<ActionResult<UsuarioAdmin>> GetUsuarioAdmin(Guid id)  // ✅ CAMBIO: int → Guid
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
        public async Task<ActionResult> Login([FromBody] LoginRequestDto request)  // ✅ CAMBIO: usa DTO del archivo
        {
            // Login es el correo según tu DTO
            var usuario = await _context.UsuariosAdmin
                .FirstOrDefaultAsync(u => u.CorreoAdm == request.Login && u.PasswAdm == request.Password);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            // Actualizar timestamp de último login
            usuario.LogintAdm = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                usuario.IdAdm,
                usuario.NombreAdm,
                usuario.CorreoAdm,
                usuario.IdOrgAdm
            });
        }

        // POST: api/UsuariosAdmin
        [HttpPost]
        public async Task<ActionResult<UsuarioAdmin>> PostUsuarioAdmin(UsuarioAdminCreateDto dto)  // ✅ CAMBIO: usa DTO
        {
            // Verificar que el correo no exista
            if (await _context.UsuariosAdmin.AnyAsync(u => u.CorreoAdm == dto.CorreoAdm))
            {
                return BadRequest(new { message = "El correo ya está registrado" });
            }

            var usuario = new UsuarioAdmin
            {
                NombreAdm = dto.NombreAdm,
                CorreoAdm = dto.CorreoAdm,
                IdOrgAdm = dto.IdOrgAdm,
                PasswAdm = dto.PassAdm,  // TODO: Hash password en producción
                CreadoAdm = DateTime.UtcNow
            };

            _context.UsuariosAdmin.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioAdmin), new { id = usuario.IdAdm }, usuario);
        }

        // PUT: api/UsuariosAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioAdmin(Guid id, UsuarioAdminUpdateDto dto)  // ✅ CAMBIO: Guid, usa DTO
        {
            var usuario = await _context.UsuariosAdmin.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar que el correo no esté en uso por otro usuario
            if (await _context.UsuariosAdmin.AnyAsync(u => u.CorreoAdm == dto.CorreoAdm && u.IdAdm != id))
            {
                return BadRequest(new { message = "El correo ya está en uso" });
            }

            usuario.NombreAdm = dto.NombreAdm;
            usuario.CorreoAdm = dto.CorreoAdm;
            usuario.IdOrgAdm = dto.IdOrgAdm;

            // Solo actualizar contraseña si se proporciona
            if (!string.IsNullOrEmpty(dto.PassAdm))
            {
                usuario.PasswAdm = dto.PassAdm;  // TODO: Hash password en producción
            }

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
        public async Task<IActionResult> DeleteUsuarioAdmin(Guid id)  // ✅ CAMBIO: int → Guid
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

        private bool UsuarioAdminExists(Guid id)  // ✅ CAMBIO: int → Guid
        {
            return _context.UsuariosAdmin.Any(e => e.IdAdm == id);
        }
    }
}