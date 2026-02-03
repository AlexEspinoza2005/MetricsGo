using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("chatbots")]
    public class Chatbot
    {
        [Key]
        [Column("idbot")]
        public Guid IdBot { get; set; } = Guid.NewGuid();

        [Required]
        [Column("idorg_bot")]
        public Guid IdOrgBot { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombrebot")]
        public string NombreBot { get; set; } = string.Empty;

        [Column("creadobot")]
        public DateTime CreadoBot { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdOrgBot")]
        public Organizacion? Organizacion { get; set; }

        public ICollection<Conversacion>? Conversaciones { get; set; }
    }
}