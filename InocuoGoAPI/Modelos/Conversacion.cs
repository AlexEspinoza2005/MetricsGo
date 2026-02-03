using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("conversaciones")]
    public class Conversacion
    {
        [Key]
        [Column("idcon")]
        public int IdCon { get; set; }

        [Column("idusu_con")]
        public int IdUsuCon { get; set; }

        [Column("idbot_con")]
        public int IdBotCon { get; set; }

        [Column("estadocon")]
        public string? EstadoCon { get; set; }

        [Column("iniciocon")]
        public DateTime? Iniciocon { get; set; }

        [Column("actualizacon")]
        public DateTime? ActualizaCon { get; set; }

        // Relaciones
        [ForeignKey("IdUsuCon")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdBotCon")]
        public Chatbot? Chatbot { get; set; }

        public ICollection<Mensaje>? Mensajes { get; set; }
    }
}