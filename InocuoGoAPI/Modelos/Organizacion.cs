using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("organizaciones")]
    public class Organizacion
    {
        [Key]
        [Column("idorg")]
        public int IdOrg { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombreorg")]
        public string NombreOrg { get; set; } = string.Empty;

        [Column("creadoorg")]
        public DateTime CreadoOrg { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<Chatbot>? Chatbots { get; set; }
    }
}