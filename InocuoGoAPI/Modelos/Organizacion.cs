using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("organizaciones")]
    public class Organizacion
    {
        [Key]
        [Column("idorg")]
        public Guid IdOrg { get; set; } = Guid.NewGuid();

        [Required]
        [Column("nombreorg")]
        public string NombreOrg { get; set; } = string.Empty;

        [Column("creadoorg")]
        public DateTime CreadoOrg { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<Chatbot>? Chatbots { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<UsuarioAdmin>? UsuariosAdmin { get; set; }
        public ICollection<Topico>? Topicos { get; set; }
    }
}