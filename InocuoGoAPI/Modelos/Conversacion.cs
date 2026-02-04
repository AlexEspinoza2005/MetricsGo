using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("conversaciones")]
    public class Conversacion
    {
        [Key]
        [Column("idcon")]
        public Guid IdCon { get; set; } = Guid.NewGuid();

        [Required]
        [Column("idbot_con")]
        public Guid IdBotCon { get; set; }

        [Required]
        [Column("idusu_con")]
        public long IdUsuCon { get; set; }

        [Required]
        [Column("estadocon")]
        public string EstadoCon { get; set; } = "abierta"; 

        [Column("iniciocon")]
        public DateTime Iniciocon { get; set; } = DateTime.UtcNow;

        [Column("actualizacon")]
        public DateTime ActualizaCon { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdBotCon")]
        public Chatbot? Chatbot { get; set; }

        [ForeignKey("IdUsuCon")]
        public Usuario? Usuario { get; set; }

        public ICollection<Mensaje>? Mensajes { get; set; }
    }
}