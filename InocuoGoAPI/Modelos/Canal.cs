using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("canales")]
    public class Canal
    {
        [Key]
        [Column("idcan")]
        public int IdCan { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombrecan")]
        public string NombreCan { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Conversacion>? Conversaciones { get; set; }
    }
}