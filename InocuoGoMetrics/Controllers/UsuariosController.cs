using InocuoGoMetrics.Models;
using Microsoft.AspNetCore.Mvc;

namespace InocuoGoMetrics.Controllers
{
    public class UsuariosController : Controller
    {
        // Lista estática en memoria
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan@mail.com", Password = "123", Rol = "Admin", Estado = "Activo" },
            new Usuario { Id = 2, Nombre = "María López", Correo = "maria@mail.com", Password = "456", Rol = "Analista", Estado = "Activo" },
            new Usuario { Id = 3, Nombre = "Carlos Ruiz", Correo = "carlos@mail.com", Password = "789", Rol = "Analista", Estado = "Inactivo" }
        };

        // GET: Usuarios
        public IActionResult Index()
        {
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
                usuarios.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // POST: Usuarios/CambiarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarEstado(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                // Alternar entre Activo e Inactivo
                usuario.Estado = usuario.Estado == "Activo" ? "Inactivo" : "Activo";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}