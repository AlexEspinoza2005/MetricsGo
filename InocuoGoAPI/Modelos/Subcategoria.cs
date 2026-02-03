using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("subcategorias")]
    public class Subcategoria
    {
        [Key]
        [Column("idsub")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdSub { get; set; } // BIGSERIAL

        [Required]
        [Column("idtem_sub")]
        public long IdTemSub { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombresub")]
        public string NombreSub { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("descrisub")]
        public string? DescriSub { get; set; }

        [Column("activosub")]
        public bool ActivoSub { get; set; } = true;

        [Column("creadosub")]
        public DateTime CreadoSub { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdTemSub")]
        public Topico? Topico { get; set; }

        public ICollection<Clasificacion>? Clasificaciones { get; set; }
    }
}