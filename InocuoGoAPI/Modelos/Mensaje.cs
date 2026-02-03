using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("mensajes")]
    public class Mensaje
    {
        [Key]
        [Column("idmen")]
        public int IdMen { get; set; }

        [Column("idcon_men")]
        public int IdConMen { get; set; }

        [Required]
        [Column("tipomen")]
        public string TipoMen { get; set; } = string.Empty;

        [Required]
        [Column("cuerpomen")]
        public string CuerpoMen { get; set; } = string.Empty;

        [Column("direccionmen")]
        public string? DireccionMen { get; set; }

        [Column("creadomen")]
        public DateTime CreadoMen { get; set; } = DateTime.UtcNow;

        [Column("idcon_mer")]
        public string? IdconMer { get; set; }

        // Relaciones
        [ForeignKey("IdConMen")]
        public Conversacion? Conversacion { get; set; }

        public ICollection<Clasificacion>? Clasificaciones { get; set; }
    }
}