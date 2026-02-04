using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("canales")]
    public class Canal
    {
        [Key]
        [Column("idcan")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short IdCan { get; set; } 

        [Required]
        [Column("nombrecan")]
        public string NombreCan { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}