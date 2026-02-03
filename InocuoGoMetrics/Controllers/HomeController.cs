using InocuoGoMetrics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InocuoGoMetrics.Controllers
{
    public class HomeController : Controller
    {
        // Datos temporales
        private static List<Topico> topicos = new List<Topico>
        {
            new Topico { Id = 1, Titulo = "Higiene Personal", Descripcion = "Procedimientos de lavado de manos y uso de desinfectantes", Icono = "fa-hands-wash" },
            new Topico { Id = 2, Titulo = "Control de Temperaturas", Descripcion = "Monitoreo y registro de temperaturas en equipos de refrigeración", Icono = "fa-temperature-high" },
            new Topico { Id = 3, Titulo = "Equipos de Protección", Descripcion = "Uso correcto de guantes, mascarillas y uniformes", Icono = "fa-shield-alt" },
            new Topico { Id = 4, Titulo = "Almacenamiento Seguro", Descripcion = "Técnicas de almacenamiento para prevenir contaminación", Icono = "fa-warehouse" }
        };

        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan.perez@example.com", Rol = "Admin", Estado = "Activo" },
            new Usuario { Id = 2, Nombre = "María García", Correo = "maria.garcia@example.com", Rol = "Analista", Estado = "Activo" },
            new Usuario { Id = 3, Nombre = "Carlos López", Correo = "carlos.lopez@example.com", Rol = "Analista", Estado = "Inactivo" },
            new Usuario { Id = 4, Nombre = "Ana Martínez", Correo = "ana.martinez@example.com", Rol = "Analista", Estado = "Activo" },
            new Usuario { Id = 5, Nombre = "Pedro Rodríguez", Correo = "pedro.rodriguez@example.com", Rol = "Analista", Estado = "Inactivo" }
        };

        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalConversaciones = 123, // Dato hardcodeado por ahora
                UsuariosUnicos = 123,
                TemasRecurrentes = new Dictionary<string, int>
                {
                    { "Lavado de Manos", 40 },
                    { "Temperaturas", 30 },
                    { "Equipos de Protección", 15 },
                    { "Almacenamiento", 10 },
                    { "Otros", 5 }
                }
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult EditarPerfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditarPerfil(string nombre, string correo, string passwordActual, string passwordNuevo)
        {
            // Aquí irá la lógica cuando conectes a Neon
            ViewBag.Success = "Perfil actualizado correctamente";
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}