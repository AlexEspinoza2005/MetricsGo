using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("chatbots")]
    public class Chatbot
    {
        [Key]
        [Column("idbot")]
        public int IdBot { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombrebot")]
        public string NombreBot { get; set; } = string.Empty;

        [Column("idorg_bot")]
        public int IdOrgBot { get; set; }

        [Column("creadobot")]
        public DateTime CreadoBot { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdOrgBot")]
        public Organizacion? Organizacion { get; set; }

        public ICollection<Conversacion>? Conversaciones { get; set; }
    }
}