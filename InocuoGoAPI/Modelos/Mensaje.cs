using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("mensajes")]
    public class Mensaje
    {
        [Key]
        [Column("idmen")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdMen { get; set; }

        [Required]
        [Column("idcon_men")]
        public Guid IdConMen { get; set; }

        [Required]
        [Column("direccionmen")]
        public string DireccionMen { get; set; } = string.Empty; 

        [Required]
        [Column("tipomen")]
        public string TipoMen { get; set; } = "texto";

        [Column("cuerpomen")]
        public string? CuerpoMen { get; set; }

        [Column("creadomen")]
        public DateTime CreadoMen { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdConMen")]
        public Conversacion? Conversacion { get; set; }

        public ICollection<Clasificacion>? Clasificaciones { get; set; }
    }
}